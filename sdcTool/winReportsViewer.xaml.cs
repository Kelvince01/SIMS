using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CrystalDecisions.CrystalReports.Engine;
using sdcTool.msgItems;

namespace sdcTool
{
    /// <summary>
    /// Interaction logic for winReportsViewer.xaml
    /// </summary>
    public partial class winReportsViewer : Window
    {
        private winMsdslVatSync _parent1;
        private string counterId;
        private cryptHelp m_cryptHelp;
        private string ip;
        private int port;
        private tcpHelp m_tcpHelp;
        private bool isReturn3 = false;
        private threadStatus endReponse = new threadStatus();
        private List<CreateXRes.dataItems> xReportsData = new List<CreateXRes.dataItems>();
        private int m_iOffset = 0;

        public winReportsViewer(string _counterId,
            tcpHelp _m_tcpHelp,
            cryptHelp _m_cryptHelp,
            string _ip,
            int _port)
        {
            InitializeComponent();
            this.counterId = _counterId;
            this.m_cryptHelp = _m_cryptHelp;
            this.ip = _ip;
            this.port = _port;
            this.m_tcpHelp = _m_tcpHelp;
        }

        public int getEndRespon(int iRet, string strRt)
        {
            this.endReponse = new threadStatus();
            this.endReponse.status = iRet;
            this.endReponse.message = strRt;
            this.isReturn3 = true;
            return 0;
        }

        private void XReportPeriodic(int iOffset)
        {
            try
            {
                CreateXReq createXreq = new CreateXReq()
                {
                    cashierID = this.counterId,
                    data = "{\"startDate\":\"" + this.dtFromDate.SelectedDate?.ToString("yyyyMMdd") + "\",\"endDate\":\"" + this.dtToDate.SelectedDate?.ToString("yyyyMMdd") + "\",\"limit\":\"10\",\"offset\":\"" + (object)iOffset + "\"}",
                    type = "SDCA0007"
                };
                createXreq.checkCode = this.m_cryptHelp.sha256(createXreq.data).ToLower();
                byte[] by = new MainWindow().packSendData(JsonHelper.SerializeObjct((object)createXreq));
                this.isReturn3 = false;
                if (!this.m_tcpHelp.SendConnection(this.ip, this.port, new tcpHelp.SomeFunWay(this.getEndRespon)))
                {
                    int num1 = (int)MessageBox.Show("connect tcp server fail. ip = " + this.ip + " port = " + (object)this.port);
                }
                else
                {
                    this.m_tcpHelp.Send(by, by.Length);
                    do
                        ;
                    while (!this.isReturn3);
                    threadStatus endReponse = this.endReponse;
                    this.m_tcpHelp.closeClient();
                    if (0 == endReponse.status)
                    {
                        CreateXRes createXres = JsonHelper.JsonConvertObject<CreateXRes>(new MainWindow().parseRecvData(endReponse.message));
                        if (createXres.checkCode != this.m_cryptHelp.sha256(createXres.data).ToLower())
                        {
                            int num2 = (int)MessageBox.Show("check code error." + Environment.NewLine + "end get version");
                        }
                        else if ("0000" != createXres.code)
                        {
                            int num3 = (int)MessageBox.Show("SDC error code : " + createXres.code + " ," + createXres.msg);
                        }
                        else
                        {
                            createXres.data = createXres.data.Replace("{\"list\":", "");
                            int length = createXres.data.IndexOf(",\"total\":");
                            string str = createXres.data.Substring(length + 1, createXres.data.Length - length - 2);
                            createXres.total = Convert.ToInt32(str.Split(':')[1].Replace("\"", ""));
                            createXres.data = createXres.data.Substring(0, length);
                            createXres.list = JsonHelper.JsonConvertObject<List<CreateXRes.dataItems>>(createXres.data);
                            foreach (CreateXRes.dataItems dataItems in createXres.list)
                                this.xReportsData.Add(dataItems);
                            if (this.xReportsData.Count < createXres.total)
                            {
                                this.m_iOffset += createXres.list.Count;
                                this.XReportPeriodic(this.m_iOffset);
                            }
                            if (createXres != null)
                            {
                                ReportDocument reportDocument = new ReportDocument();
                                reportDocument.Load("rptXInquiryPeriodic.rpt");
                                reportDocument.SetDataSource((IEnumerable)this.xReportsData);
                                this.crystalReportViewer1.ViewerCore.ReportSource = (object)reportDocument;
                                //this.crystalReportViewer1.Show();
                                this.crystalReportViewer1.ViewerCore.ShowFirstPage();
                                this.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void BtnView_OnClick(object sender, RoutedEventArgs e)
        {
            if ((bool)this.rbCashier.IsChecked)
            {
                try
                {
                    CreateXReq createXreq = new CreateXReq()
                    {
                        cashierID = this.counterId,
                        checkCode = "EC2AE92056B867C3FF725BA974363802093B96F3E4E94FFAFD7A3A9200088264",
                        data = "SDCA0006",
                        type = "SDCA0006"
                    };
                    createXreq.checkCode = this.m_cryptHelp.sha256(createXreq.data).ToLower();
                    byte[] by = new MainWindow().packSendData(JsonHelper.SerializeObjct((object)createXreq));
                    this.isReturn3 = false;
                    if (!this.m_tcpHelp.SendConnection(this.ip, this.port, new tcpHelp.SomeFunWay(this.getEndRespon)))
                    {
                        int num1 = (int)MessageBox.Show("connect tcp server fail. ip = " + this.ip + " port = " + (object)this.port);
                    }
                    else
                    {
                        this.m_tcpHelp.Send(by, by.Length);
                        do
                            ;
                        while (!this.isReturn3);
                        threadStatus endReponse = this.endReponse;
                        this.m_tcpHelp.closeClient();
                        if (0 != endReponse.status)
                            return;
                        CreateXRes createXres = JsonHelper.JsonConvertObject<CreateXRes>(new MainWindow().parseRecvData(endReponse.message));
                        if (createXres.checkCode != this.m_cryptHelp.sha256(createXres.data).ToLower())
                        {
                            int num2 = (int)MessageBox.Show("check code error." + Environment.NewLine + "end get version");
                        }
                        else if ("0000" != createXres.code)
                        {
                            int num3 = (int)MessageBox.Show("SDC error code : " + createXres.code + " ," + createXres.msg);
                        }
                        else if (createXres != null)
                        {
                            CreateXRes.dataItems dataItems = JsonHelper.JsonConvertObject<CreateXRes.dataItems>(createXres.data);
                            ReportDocument reportDocument = new ReportDocument();
                            List<CreateXRes.dataItems> dataItemsList = new List<CreateXRes.dataItems>();
                            dataItemsList.Add(dataItems);
                            reportDocument.Load("rptXReportCashier.rpt");
                            reportDocument.SetDataSource((IEnumerable)dataItemsList);
                            this.crystalReportViewer1.ViewerCore.ReportSource = (object)reportDocument;
                            this.crystalReportViewer1.ViewerCore.ShowFirstPage();
                            this.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if ((bool)!this.rbPeriodical.IsChecked)
                    return;
                this.m_iOffset = 0;
                this.XReportPeriodic(this.m_iOffset);
                this.xReportsData = new List<CreateXRes.dataItems>();
            }
        }
    }
}
