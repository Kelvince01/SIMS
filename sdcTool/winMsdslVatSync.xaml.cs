using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using sdcTool.Models;
using sdcTool.msgItems;

namespace sdcTool
{
    /// <summary>
    /// Interaction logic for winMsdslVatSync.xaml
    /// </summary>
    public partial class winMsdslVatSync : Window
    {
        private bool isConfigFound = false;
        private bool isInitServer = false;
        public cryptHelp m_cryptHelp = new cryptHelp();
        public udpHelp m_udpHelp = new udpHelp();
        public tcpHelp m_tcpHelp = new tcpHelp();
        private string strKey = "";
        private string strSrc = "";
        private string strRet = "";
        private byte[] m_keyByte;
        private Dictionary<string, string> devMap = new Dictionary<string, string>();
        public List<SDC_SD> sdMap = new List<SDC_SD>();
        public List<SDC_VAT> vatMap = new List<SDC_VAT>();
        public string ip;
        public string DeviceSN;
        public int udp_port;
        public int tcp_port;
        public string sn;
        public string counterId = "";
        public string deviceSN = "";
        private int isReturn = 1;
        private bool isReturn2 = false;
        public bool isReturn3 = false;
        public bool isInitializeIP = false;
        public bool isInitializeSD = false;
        public bool isInitializeVAT = false;
        public bool isInitializeRunning = true;
        private threadStatus versionReponse = new threadStatus();
        public threadStatus endReponse = new threadStatus();

        public winMsdslVatSync()
        {
            InitializeComponent();
            //this.panelActivation.Size = new Size(420, 293);
            try
            {
                this.DeviceSN = ConfigurationManager.AppSettings[nameof(DeviceSN)].ToString();
                this.udp_port = int.Parse(ConfigurationManager.AppSettings[nameof(udp_port)].ToString());
                this.tcp_port = int.Parse(ConfigurationManager.AppSettings[nameof(tcp_port)].ToString());
                this.sn = ConfigurationManager.AppSettings[nameof(sn)].ToString();
                this.counterId = ConfigurationManager.AppSettings["CounterId"].ToString();
                this.deviceSN = ConfigurationManager.AppSettings[nameof(DeviceSN)].ToString();
                this.isConfigFound = true;
                this.strKey = new MainWindow().getKey("SZZT002197");
                this.strSrc = "SZZT|" + this.sn;
                this.m_keyByte = this.m_cryptHelp.HexStringToByteArray(this.strKey);
                this.strRet = this.m_cryptHelp.AesEncrypt(this.strSrc, this.m_keyByte);
            }
            catch (Exception ex)
            {
                //this.rtbError.Text = ex.Message;
            }
        }

        public int recvFunUDP(int iRet, string strRt)
        {
            ++this.isReturn;
            if (iRet == 0)
            {
                string[] strArray = this.m_cryptHelp.AesDecrypt(strRt, this.m_keyByte).Split('|');
                this.addDev(strArray[3] + "_" + strArray[2], strArray[1]);
            }
            //else
            //  this.rtbError.Text = strRt;
            return 0;
        }

        public void addDev(string strKey, string data)
        {
            if (this.devMap.ContainsKey(strKey))
                return;
            this.devMap.Add(strKey, data);
        }

        private void WinMsdslVatSync_OnLoaded(object sender, RoutedEventArgs e)
        {
            //this.timerLoad.Enabled = true;
        }

        public int getVersionRespon(int iRet, string strRt)
        {
            this.versionReponse = new threadStatus();
            this.versionReponse.status = iRet;
            this.versionReponse.message = strRt;
            this.isReturn2 = true;
            return 0;
        }

        public int getEndRespon(int iRet, string strRt)
        {
            this.endReponse = new threadStatus();
            this.endReponse.status = iRet;
            this.endReponse.message = strRt;
            this.isReturn3 = true;
            return 0;
        }

        /*
        public void beginGetSD(int offset)
        {
            try
            {
                if (offset == 0)
                    this.sdMap.Clear();
                getSDReq getSdReq = new getSDReq()
                {
                    cashierID = this.counterId,
                    data = JsonHelper.SerializeObjct((object)new getSDReq.dataItems()
                    {
                        limit = "10",
                        offset = offset.ToString()
                    }),
                    type = "SDCA0013"
                };
                getSdReq.checkCode = this.m_cryptHelp.sha256(getSdReq.data).ToLower();
                byte[] by = new MainWindow().packSendData(JsonHelper.SerializeObjct((object)getSdReq));
                this.isReturn3 = false;
                if (!this.m_tcpHelp.SendConnection(this.ip, this.tcp_port, new tcpHelp.SomeFunWay(this.getEndRespon)))
                {
                    this.rtbError.Text = "connect tcp server fail. ip = " + this.ip + " port = " + (object)this.tcp_port;
                    this.isInitializeRunning = false;
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
                        getSDRes getSdRes = JsonHelper.JsonConvertObject<getSDRes>(new MainWindow().parseRecvData(endReponse.message));
                        if (getSdRes.checkCode != this.m_cryptHelp.sha256(getSdRes.data).ToLower())
                        {
                            this.rtbError.Text = "check code error." + Environment.NewLine + "end get version";
                            this.isInitializeRunning = false;
                        }
                        else if ("0000" != getSdRes.code)
                        {
                            this.rtbError.Text += "SDC get sd error.";
                            RichTextBox rtbError = this.rtbError;
                            rtbError.Text = rtbError.Text + "SDC error code : " + getSdRes.code + " ," + getSdRes.msg;
                            this.isInitializeRunning = false;
                        }
                        else
                        {
                            getSDRes.dataItems dataItems = JsonHelper.JsonConvertObject<getSDRes.dataItems>(getSdRes.data);
                            for (int index = 0; index < dataItems.rates.Count; ++index)
                            {
                                getSDRes.RatesItem rate = dataItems.rates[index];
                                string str = rate.serviceName + "_" + rate.sdRate + "_" + rate.id;
                                this.sdMap.Add(new SDC_SD()
                                {
                                    Text = str,
                                    Value = rate.id,
                                    SDRate = rate.sdRate
                                });
                            }
                            if (this.sdMap.Count < int.Parse(dataItems.total))
                            {
                                this.beginGetSD(this.sdMap.Count);
                            }
                            else
                            {
                                this.lblSDCode.Text = "SD Code Found : " + this.sdMap.Count.ToString();
                                this.isInitializeSD = true;
                                this.beginGetVat(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.rtbError.Text = "SD Initilization error" + Environment.NewLine + ex.Message;
            }
        }

        public void beginGetVat(int iOffset)
        {
            try
            {
                this.lblStatus.Text = "Initializing Vat Data";
                Application.DoEvents();
                Thread.Sleep(1000);
                getTaxRateReq getTaxRateReq = new getTaxRateReq()
                {
                    cashierID = this.counterId,
                    checkCode = "5AEAD1D3D154FE1D39836A37023663DF9BE0DFEF9398EBD4141AA0A0A9DA07F3",
                    data = JsonHelper.SerializeObjct((object)new getSDReq.dataItems()
                    {
                        limit = "10",
                        offset = iOffset.ToString()
                    }),
                    type = "SDCA0012"
                };
                getTaxRateReq.checkCode = this.m_cryptHelp.sha256(getTaxRateReq.data).ToLower();
                byte[] by = new MainWindow().packSendData(JsonHelper.SerializeObjct((object)getTaxRateReq));
                this.isReturn3 = false;
                if (!this.m_tcpHelp.SendConnection(this.ip, this.tcp_port, new tcpHelp.SomeFunWay(this.getEndRespon)))
                {
                    this.rtbError.Text = "connect tcp server fail. ip = " + this.ip + " port = " + (object)this.tcp_port;
                    this.isInitializeRunning = false;
                }
                else
                {
                    this.m_tcpHelp.Send(by, by.Length);
                    do
                        ;
                    while (!this.isReturn3);
                    threadStatus endReponse = this.endReponse;
                    this.m_tcpHelp.closeClient();
                    if (endReponse.status == 0)
                    {
                        getTaxRateRes getTaxRateRes = JsonHelper.JsonConvertObject<getTaxRateRes>(new MainWindow().parseRecvData(endReponse.message));
                        if (getTaxRateRes.checkCode != this.m_cryptHelp.sha256(getTaxRateRes.data).ToLower())
                        {
                            this.rtbError.Text = "check code error." + Environment.NewLine + "end get vat";
                            this.isInitializeRunning = false;
                        }
                        else if ("0000" != getTaxRateRes.code)
                        {
                            this.rtbError.Text += "SDC vat error ";
                            RichTextBox rtbError = this.rtbError;
                            rtbError.Text = rtbError.Text + "SDC error code : " + getTaxRateRes.code + " ," + getTaxRateRes.msg;
                            this.isInitializeRunning = false;
                        }
                        else
                        {
                            getTaxRateRes.dataItems dataItems = JsonHelper.JsonConvertObject<getTaxRateRes.dataItems>(getTaxRateRes.data);
                            for (int index = 0; index < dataItems.rates.Count; ++index)
                            {
                                getTaxRateRes.RatesItem rate = dataItems.rates[index];
                                string str = rate.serviceName + "_" + rate.vatRate + "_" + rate.id;
                                this.vatMap.Add(new SDC_VAT()
                                {
                                    Text = str,
                                    Value = rate.id,
                                    VatRate = rate.vatRate
                                });
                            }
                            if (this.vatMap.Count < int.Parse(dataItems.total))
                            {
                                this.beginGetVat(this.vatMap.Count);
                            }
                            else
                            {
                                this.isInitializeVAT = true;
                                this.isInitializeRunning = false;
                                this.lblVatCode.Text = "VAT Code Found : " + this.vatMap.Count.ToString();
                                this.timerDataSync.Enabled = true;
                                this.timerInvoice.Enabled = true;
                                this.btnZReport.Enabled = true;
                                this.btnReports.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.rtbError.Text = "VAT Initilization error" + Environment.NewLine + ex.Message;
            }
        }

        public invoiceRes.dataItems PostSalesDataToSDC(invoiceReq.dataItems dataItem)
        {
            try
            {
                this.lblStatus.Text = "Checking for new sales data ....";
                Application.DoEvents();
                invoiceReq invoiceReq = new invoiceReq()
                {
                    cashierID = this.counterId,
                    data = JsonHelper.SerializeObjct((object)dataItem),
                    type = "SDCA0000"
                };
                invoiceReq.checkCode = this.m_cryptHelp.sha256(invoiceReq.data).ToLower();
                byte[] by = new MainWindow().packSendData(JsonHelper.SerializeObjct((object)invoiceReq));
                this.isReturn3 = false;
                if (!this.m_tcpHelp.SendConnection(this.ip, this.tcp_port, new tcpHelp.SomeFunWay(this.getEndRespon)))
                {
                    this.rtbError.Text = "connect tcp server fail. ip = " + this.ip + " port = " + (object)this.tcp_port;
                    this.timerDataSync.Enabled = true;
                    return (invoiceRes.dataItems)null;
                }
                this.m_tcpHelp.Send(by, by.Length);
                this.lblStatus.Text = "Sending Sales data to  SDC....";
                Application.DoEvents();
                do
                    ;
                while (!this.isReturn3);
                this.m_tcpHelp.closeClient();
                threadStatus endReponse = this.endReponse;
                if (endReponse.status == 0)
                {
                    invoiceRes invoiceRes = JsonHelper.JsonConvertObject<invoiceRes>(new MainWindow().parseRecvData(endReponse.message));
                    if (invoiceRes.checkCode != this.m_cryptHelp.sha256(invoiceRes.data).ToLower())
                    {
                        this.rtbError.Text = "invoice error.";
                        this.timerDataSync.Enabled = true;
                        return (invoiceRes.dataItems)null;
                    }
                    if ("0000" != invoiceRes.code)
                    {
                        this.rtbError.Text = "SDC invoice error.";
                        this.rtbError.Text = "SDC error code : " + invoiceRes.code + " ," + invoiceRes.msg;
                        this.timerDataSync.Enabled = true;
                        return (invoiceRes.dataItems)null;
                    }
                    this.lblStatus.Text = "Sending Sales data Posting Successfully....";
                    Application.DoEvents();
                    return JsonHelper.JsonConvertObject<invoiceRes.dataItems>(invoiceRes.data);
                }
                this.rtbError.Text = "Connection error." + Environment.NewLine + "end invoice. " + endReponse.message;
                this.timerDataSync.Enabled = true;
                return (invoiceRes.dataItems)null;
            }
            catch (Exception ex)
            {
                this.rtbError.Text = ex.Message;
                return (invoiceRes.dataItems)null;
            }
        }

        public creditNoteRes.dataItems PostSaleReturnDataToSDC(
          creditNoteReq.dataItems dataItem)
        {
            try
            {
                this.lblStatus.Text = "Checking for new sales Return data ....";
                Application.DoEvents();
                creditNoteReq creditNoteReq = new creditNoteReq()
                {
                    cashierID = this.counterId,
                    data = JsonHelper.SerializeObjct((object)dataItem),
                    type = "SDCA0014"
                };
                creditNoteReq.checkCode = this.m_cryptHelp.sha256(creditNoteReq.data).ToLower();
                byte[] by = new MainWindow().packSendData(JsonHelper.SerializeObjct((object)creditNoteReq));
                this.isReturn3 = false;
                if (!this.m_tcpHelp.SendConnection(this.ip, this.tcp_port, new tcpHelp.SomeFunWay(this.getEndRespon)))
                {
                    this.rtbError.Text = "connect tcp server fail. ip = " + this.ip + " port = " + (object)this.tcp_port;
                    this.timerDataSync.Enabled = true;
                    return (creditNoteRes.dataItems)null;
                }
                this.m_tcpHelp.Send(by, by.Length);
                this.lblStatus.Text = "Sending Sales Return data to  SDC....";
                Application.DoEvents();
                do
                    ;
                while (!this.isReturn3);
                this.m_tcpHelp.closeClient();
                threadStatus endReponse = this.endReponse;
                if (endReponse.status == 0)
                {
                    creditNoteRes creditNoteRes = JsonHelper.JsonConvertObject<creditNoteRes>(new MainWindow().parseRecvData(endReponse.message));
                    JsonHelper.SerializeObjct((object)creditNoteRes.data);
                    if (!string.Equals(creditNoteRes.checkCode, this.m_cryptHelp.sha256(creditNoteRes.data).ToLower()))
                    {
                        this.rtbError.Text = "src: " + creditNoteRes.data;
                        RichTextBox rtbError1 = this.rtbError;
                        rtbError1.Text = rtbError1.Text + "PC checCode: " + MainWindow.strToToHexByte(this.m_cryptHelp.sha256(creditNoteRes.data).ToLower()).ToString();
                        RichTextBox rtbError2 = this.rtbError;
                        rtbError2.Text = rtbError2.Text + "server checCode: " + MainWindow.strToToHexByte(creditNoteRes.checkCode).ToString();
                        this.rtbError.Text += "creditNote checkCode error.";
                        this.rtbError.Text += "end creditNote.";
                        this.timerDataSync.Enabled = true;
                        return (creditNoteRes.dataItems)null;
                    }
                    if ("0000" != creditNoteRes.code)
                    {
                        this.rtbError.Text = "SDC creditNote error.";
                        this.rtbError.Text = "SDC error code : " + creditNoteRes.code + " ," + creditNoteRes.msg;
                        this.timerDataSync.Enabled = true;
                        return (creditNoteRes.dataItems)null;
                    }
                    creditNoteRes.dataItems dataItems = JsonHelper.JsonConvertObject<creditNoteRes.dataItems>(creditNoteRes.data);
                    for (int index = 0; index < dataItems.goodsInfo.Count; ++index)
                    {
                        creditNoteRes.GoodsInfoItem goodsInfoItem = dataItems.goodsInfo[index];
                    }
                    this.lblStatus.Text = "Sending Sales Return data Posting Successfully....";
                    Application.DoEvents();
                    return JsonHelper.JsonConvertObject<creditNoteRes.dataItems>(creditNoteRes.data);
                }
                this.rtbError.Text = "Connection error." + Environment.NewLine + "end invoice. " + endReponse.message;
                this.timerDataSync.Enabled = true;
                return (creditNoteRes.dataItems)null;
            }
            catch (Exception ex)
            {
                this.rtbError.Text = ex.Message;
                return (creditNoteRes.dataItems)null;
            }
        }
        */
    }
}
