using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.Reports
{
    /// <summary>
    /// Interaction logic for HtmlReportViewer.xaml
    /// </summary>
    public partial class HtmlReportViewer : Window
    {
        private string query;
        private IAct_MasterChartOfAccountService _serviceChar;

        public HtmlReportViewer()
        {
            InitializeComponent();
            this._serviceChar = (IAct_MasterChartOfAccountService)new Act_MasterChartOfAccountService((IDbFactory)new DbFactory());
        }

        public void SalesExpenseReport(string fromDate, string toDate, string shopId, string userId)
        {
            this.query = string.Format("SP_ReportSalesExpense '{0}','{1}','{2}','{3}' ", (object)fromDate, (object)toDate, (object)shopId, (object)userId);
            DataTable data = new SQLDAL().Select(this.query).Data;
            List<Act_MasterChartOfAccount> accountWithOutBankCode = this._serviceChar.GetCurrentYearChartOfAccountWithOutBankCode();
            if (data == null || data.Rows.Count == 0)
            {
                int num1 = (int)MessageBox.Show("No Data found");
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<html> <title> </title> <head> <style type=\"text/css\">table {\r\n                                        width:900px;\r\n                                        border:1px solid;\r\n\t\t\t\t\t\t\t\t\t\t  font-size:10px;\r\n\t\t\t\t\t\t\t\t\t\t  cellpadding:0px;\r\n                                    }\r\n\r\n\t\t\t\t\t\t\t\t\tbody {\r\n                                    font-size:10px;\r\n                                    }\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\tth{\r\n\t\t\t\t\t\t\t\t\t\tbackground-color:#74ACEC;\r\n                                        border-bottom:1px solid #000; border-right:1px solid #000;\r\n\t\t\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\ttable tr:first-child th {\r\n\t\t\t\t\t\t\t\t\t\t\tborder-bottom:1px solid;\r\n\t\t\t\t\t\t\t\t\t\t\tborder-right:1px solid;\r\n\t\t\t\t\t\t\t\t\t\t\tbackground-color:#74ACEC;\r\n\t\t\t\t\t\t\t\t\t}\r\n\r\n\t\t\t\t\t\t\t\t\ttable tr:nth-child(2) th {\r\n\t\t\t\t\t\t\t\t\t\t\tborder-bottom:1px solid;\r\n\t\t\t\t\t\t\t\t\t\t\tborder-right:1px solid;\r\n\t\t\t\t\t\t\t\t\t\t\tbackground-color:#72D9EB;\r\n\t\t\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\ttable tr td {\r\n\t\t\t\t\t\t\t\t\t\t\tborder-bottom:1px solid;\r\n\t\t\t\t\t\t\t\t\t\t\tborder-right:1px solid;\r\n\t\t\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\ttd {\r\n\t\t\t\t\t\t\t\t\t\tbackground-color:#C6F9E1;\r\n\t\t\t\t\t\t\t\t\t}\r\n\r\n\t\t\t\t\t\t\t\t\ttable td:nth-child(1)  ,\r\n\t\t\t\t\t\t\t\t\ttable td:nth-child(2) ,\r\n\t\t\t\t\t\t\t\t\ttable td:nth-child(3) ,\r\n\t\t\t\t\t\t\t\t\ttable td:nth-child(4) , \r\n\t\t\t\t\t\t\t\t\ttable td:nth-child(5) ,\r\n\t\t\t\t\t\t\t\t\ttable td:nth-child(6) ,\r\n\t\t\t\t\t\t\t\t\ttable td:nth-child(7) , \r\n\t\t\t\t\t\t\t\t\ttable td:nth-child(8) ,\r\n\t\t\t\t\t\t\t\t\ttable td:nth-child(9)\r\n\t\t\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\t\t\tbackground-color:#F8CBC5;\r\n\t\t\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\ttable td:nth-last-child(2) ,\r\n\t\t\t\t\t\t\t\t\ttable td:nth-last-child(1)\r\n\t\t\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\t\t\tbackground-color:#F4C3F7;\r\n\t\t\t\t\t\t\t\t\t}\r\n\r\n\t\t\t\t\t\t\t</style>\r\n\r\n                                </style>\r\n                        </head> <body> ");
                stringBuilder.Append(" <div> ");
                stringBuilder.Append(" <table> ");
                stringBuilder.Append(" <tr>  <th colspan=\"9\" style=\" border-bottom:1px solid #000; border-right:1px solid #000; \"> Sales </th>  <th colspan=\"" + (object)(accountWithOutBankCode.Count + 1) + "%\"> Expense </th> <th>  Balance Forward </th> <th> " + data.Rows[0]["Balance"].ToString() + " </th> </tr> ");
                stringBuilder.Append("<tr>");
                int num2 = 0;
                foreach (DataColumn column in (InternalDataCollectionBase)data.Columns)
                {
                    string s = column.ColumnName;
                    if (num2 > 9 && s != "TotalDepositExpense" && s != "Balance")
                    {
                        s = s.Substring(1, s.Length - 1);
                        Decimal actCode = Decimal.Parse(s);
                        Act_MasterChartOfAccount masterChartOfAccount = accountWithOutBankCode.Find((Predicate<Act_MasterChartOfAccount>)(m => m.ActCode == actCode));
                        if (masterChartOfAccount != null)
                            s = masterChartOfAccount.ActName;
                    }
                    string str = string.Concat(Enumerable.Select<char, string>((IEnumerable<char>)s, (Func<char, string>)(x => char.IsUpper(x) ? " " + (object)x : x.ToString()))).TrimStart(' ');
                    stringBuilder.Append(" <th> " + str + " </th>");
                    ++num2;
                }
                stringBuilder.Append("</tr>");
                for (int index = 1; index < data.Rows.Count; ++index)
                {
                    stringBuilder.Append("<tr>  ");
                    foreach (DataColumn column in (InternalDataCollectionBase)data.Columns)
                    {
                        if (column.ColumnName == "SL")
                        {
                            int num3 = int.Parse(data.Rows[index][column.ColumnName].ToString()) - 1;
                            stringBuilder.Append("<td> " + num3.ToString() + " </td> ");
                        }
                        else
                            stringBuilder.Append("<td> " + data.Rows[index][column.ColumnName].ToString() + " </td> ");
                    }
                    stringBuilder.Append("</tr>  ");
                }
                stringBuilder.Append(" </table> ");
                stringBuilder.Append(" </div>  ");
                stringBuilder.Append(" </html>  ");
                this.webBrowser1.NavigateToString(stringBuilder.ToString());
                this.ShowDialog();
            }
        }

        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {
            //this.webBrowser1.Print();
            // Send the Print command to WebBrowser. For this code to work: add the Microsoft.mshtml .NET reference
            mshtml.IHTMLDocument2 doc = webBrowser1.Document as mshtml.IHTMLDocument2;
            doc.execCommand("Print", true, null);

            //webBrowser1.InvokeScript("execScript", new object[] { "window.print();", "JavaScript" });
        }

        private void PrintCurrentPage()
        {
            // document must be loaded for this to work
            IOleServiceProvider sp = webBrowser1.Document as IOleServiceProvider;
            if (sp != null)
            {
                Guid IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
                Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");
                const int OLECMDID_PRINT = 6;
                const int OLECMDEXECOPT_DONTPROMPTUSER = 2;

                dynamic wb; // should be of IWebBrowser2 type
                sp.QueryService(IID_IWebBrowserApp, IID_IWebBrowser2, out wb);
                if (wb != null)
                {
                    // this will send to the default printer
                    wb.ExecWB(OLECMDID_PRINT, OLECMDEXECOPT_DONTPROMPTUSER, null, null);
                }
            }
        }
    }

    [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOleServiceProvider
    {
        [PreserveSig]
        int QueryService([MarshalAs(UnmanagedType.LPStruct)] Guid guidService, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out object ppvObject);
    }
}
