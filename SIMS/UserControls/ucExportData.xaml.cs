using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using SIMS.Data.Infrastructure;
using SIMS.Service;

namespace SIMS.UserControls
{
    /// <summary>
    /// Interaction logic for ucExportData.xaml
    /// </summary>
    public partial class ucExportData : BaseUserControl
    {
        //private IBuyService _serviceBuy;
        private IStyleSizeService _service;

        private Result result = new Result();

        public ucExportData()
        {
            this.Init();
            InitializeComponent();
        }

        private void Init()
        {
            IDbFactory idbFactory = (IDbFactory)new DbFactory();
            this._service = (IStyleSizeService)new StyleSizeService(idbFactory);
            //this._serviceBuy = (IBuyService)new BuyService(idbFactory);
        }

        public event ucExportData.afterCloseClick onCloseClick;

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.onCloseClick == null)
                return;
            this.onCloseClick((object)this);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Init();
            string type = (bool)!this.rbBuy.IsChecked ? "STYLE" : "BUY";
            //Cursor.Current = Cursors.WaitCursor;
            /*DataTable weghItemProduct = this._serviceBuy.GetWeghItemProduct(type);
            if (weghItemProduct == null)
            {
                int num1 = (int)MessageBox.Show("No item found");
            }
            else
            {
                OpenFileDialog folderBrowserDialog = new OpenFileDialog();
                folderBrowserDialog.ShowDialog();
                if (!string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    //weghItemProduct.TableName = "PLU";
                    string[] strArray1 = new string[5]
                    {
            "\\PLU",
            null,
            null,
            null,
            null
                    };
                    string[] strArray2 = strArray1;
                    DateTime now = DateTime.Now;
                    string str1 = now.Year.ToString();
                    strArray2[1] = str1;
                    string[] strArray3 = strArray1;
                    now = DateTime.Now;
                    string str2 = now.Month.ToString();
                    strArray3[2] = str2;
                    string[] strArray4 = strArray1;
                    now = DateTime.Now;
                    string str3 = now.Day.ToString();
                    strArray4[3] = str3;
                    strArray1[4] = ".xls";
                    string str4 = string.Concat(strArray1);
                    this.ExportData(weghItemProduct, folderBrowserDialog.SelectedPath + str4);
                }
                //Cursor.Current = Cursors.Default;
                int num2 = (int)MessageBox.Show("Save Successfully");
            }*/
        }

        private void ExportData(DataTable dt, string destination)
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(destination, (SpreadsheetDocumentType)0))
            {
                spreadsheetDocument.AddWorkbookPart();
                spreadsheetDocument.WorkbookPart.Workbook = new Workbook();
                spreadsheetDocument.WorkbookPart.Workbook.Sheets = new Sheets();
                WorksheetPart worksheetPart = ((OpenXmlPartContainer)spreadsheetDocument.WorkbookPart).AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(new OpenXmlElement[1]
                {
          (OpenXmlElement) sheetData
                });
                Sheets firstChild = ((OpenXmlElement)spreadsheetDocument.WorkbookPart.Workbook).GetFirstChild<Sheets>();
                string idOfPart = ((OpenXmlPartContainer)spreadsheetDocument.WorkbookPart).GetIdOfPart((OpenXmlPart)worksheetPart);
                uint num = 1;
                if (((OpenXmlElement)firstChild).Elements<Sheet>().Count<Sheet>() > 0)
                    num = Enumerable.Select<Sheet, uint>(((OpenXmlElement)firstChild).Elements<Sheet>(), (Func<Sheet, uint>)(s => ((OpenXmlSimpleValue<uint>)s.SheetId).Value)).Max<uint>() + 1U;
                Sheet sheet = new Sheet()
                {
                    Id = (idOfPart),
                    SheetId = (num),
                    Name = (dt.TableName)
                };
                ((OpenXmlElement)firstChild).Append(new OpenXmlElement[1]
                {
          (OpenXmlElement) sheet
                });
                Row row1 = new Row();
                List<string> stringList = new List<string>();
                foreach (DataColumn column in (InternalDataCollectionBase)dt.Columns)
                {
                    stringList.Add(column.ColumnName);
                    Cell cell = new Cell();
                    ((CellType)cell).DataType = (CellValues)4;
                    ((CellType)cell).CellValue = new CellValue(column.ColumnName);
                    ((OpenXmlElement)row1).AppendChild<Cell>(cell);
                }
                ((OpenXmlElement)sheetData).AppendChild<Row>(row1);
                foreach (DataRow row2 in (InternalDataCollectionBase)dt.Rows)
                {
                    Row row3 = new Row();
                    foreach (string columnName in stringList)
                    {
                        Cell cell = new Cell();
                        ((CellType)cell).DataType = ((CellValues)4);
                        ((CellType)cell).CellValue = new CellValue(row2[columnName].ToString());
                        ((OpenXmlElement)row3).AppendChild<Cell>(cell);
                    }
                    ((OpenXmlElement)sheetData).AppendChild<Row>(row3);
                }
            }
        }

        public delegate void afterCloseClick(object sender);
    }
}
