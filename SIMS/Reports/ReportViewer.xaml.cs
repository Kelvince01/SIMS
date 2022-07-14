using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using SIMS.Models;
using SIMS.Service;

namespace SIMS.Reports
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : Window
    {
        private string query = "";
        private IAct_MasterChartOfAccountService _service;
        private List<Act_MasterChartOfAccount> AllChartOfAcc = new List<Act_MasterChartOfAccount>();

        public ReportViewer()
        {
            InitializeComponent();
        }

        public void ShowReceiveChallanReport(
      string chlnNo,
      string userid,
      string additionalCost,
      string additionalComm,
      string challanTotal)
        {
            DataTable data;
            if (string.IsNullOrEmpty(userid))
                //data = (DataTable)new rChallanTableAdapter().GetData(chlnNo);
                data = (DataTable)new DataTable();
            else
                data = new SQLDAL().Select("SELECT        '' CmpIDX, 'DRAFT CHALLAN' Chln, TempRChallan.OrderNo, TempRChallan.sBarCode, TempRChallan.BarCode, TempRChallan.CPU, TempRChallan.RPU, TempRChallan.VPU, TempRChallan.VPP, TempRChallan.Qty, TempRChallan.bQty, TempRChallan.sQty, TempRChallan.cSqty, \r\n                                                     TempRChallan.rQty, TempRChallan.dmlqty, TempRChallan.EXPDT, TempRChallan.LastSDT, TempRChallan.ShopID, TempRChallan.Transfer," + additionalComm + " TotalPrdComm, TempRChallan.AddPrdComm, " + challanTotal + " ChlnTotal, TempRChallan.SupRef, TempRChallan.UserID, \r\n                                                     StyleSize.SSName, Product.PrdName, BrandType.BTName, PGroup.GroupName, TempRChallan.SupID, Supplier.Supname," + additionalCost + " AddiCost, 0 ACPU\r\n                            FROM            dbo.TempRChallan INNER JOIN\r\n                                                     StyleSize ON TempRChallan.BarCode = StyleSize.Barcode AND TempRChallan.sBarCode = StyleSize.sBarcode INNER JOIN\r\n                                                     PGroup ON StyleSize.GroupID = PGroup.GroupID INNER JOIN\r\n                                                     BrandType ON StyleSize.BTID = BrandType.BTID INNER JOIN\r\n                                                     Product ON StyleSize.PrdID = Product.PrdID LEFT OUTER JOIN\r\n                                                     Supplier ON TempRChallan.SupID = Supplier.SupID\r\n                            WHERE        (TempRChallan.UserID = '" + userid + "')").Data;
            string str = AppDomain.CurrentDomain.BaseDirectory + "\\Reports\\";
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(str + "rptReceiveChallan.rpt");
            reportDocument.SetDataSource(data);
            reportDocument.SetParameterValue("@ShopName", (object)StaticData.ShopName);
            reportDocument.SetParameterValue("@ShopAddr", (object)StaticData.ShopAddr);
            //this.crystalReportViewer1.ReportSource = (object)reportDocument;
            //this.crystalReportViewer1.Show();
            this.ShowDialog();
            reportDocument.Close();
            reportDocument.Dispose();
        }

        public List<Act_MasterChartOfAccount> GetChilds(Decimal id)
        {
            List<Act_MasterChartOfAccount> masterChartOfAccountList = new List<Act_MasterChartOfAccount>();
            return Enumerable.Where<Act_MasterChartOfAccount>((IEnumerable<Act_MasterChartOfAccount>)this.AllChartOfAcc, (Func<Act_MasterChartOfAccount, bool>)(m =>
            {
                Decimal? parentId = m.ParentID;
                Decimal num = id;
                return parentId.GetValueOrDefault() == num && parentId.HasValue;
            })).ToList<Act_MasterChartOfAccount>();
        }

        private void ReportViewer_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        /*public void PrintBarcode(List<BarcodeModel> dt, bool isSmall)
        {
            string str = Application.StartupPath + "\\Reports\\";
            ReportDocument reportDocument = new ReportDocument();
            if (!isSmall)
                reportDocument.Load(str + "rptBarcodePrint.rpt");
            else
                reportDocument.Load(str + "rptBarcodePrintSmall.rpt");
            reportDocument.SetDataSource((IEnumerable)dt);
            this.crystalReportViewer1.ReportSource = (object)reportDocument;
            this.crystalReportViewer1.Show();
            int num = (int)this.ShowDialog();
            reportDocument.Close();
            reportDocument.Dispose();
        }

        public void PrintPackage(List<PackageModel> dt)
        {
            string str = Application.StartupPath + "\\Reports\\";
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(str + "rptPackagePrint.rpt");
            reportDocument.SetDataSource((IEnumerable)dt);
            this.crystalReportViewer1.ReportSource = (object)reportDocument;
            this.crystalReportViewer1.Show();
            int num = (int)this.ShowDialog();
            reportDocument.Close();
            reportDocument.Dispose();
        }*/
    }
}
