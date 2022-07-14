using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.Win32;

namespace CustomControls
{
    /// <summary>
    /// Filterable Datagrid
    /// </summary>
    public class VDataGrid : DataGrid
    {
        private ICollectionView Datalist { get; set; }
        private object SearchValue { get; set; }
        public string ColumnName { get; set; }
        public bool IsFilter { get; set; }

        static VDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VDataGrid), new FrameworkPropertyMetadata(typeof(VDataGrid)));
        }

        public VDataGrid()
        {
            this.IsFilter = false;
            this.Background = new SolidColorBrush(Color.FromArgb(0, 170, 225, 239));
            this.ColumnHeaderHeight = 40;
            this.AutoGenerateColumns = false;
            CreateContextmenu();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
        {
            Datalist = CollectionViewSource.GetDefaultView(newValue);
            base.OnItemsSourceChanged(oldValue, Datalist);
            if (this.IsFilter == true)
            {
                foreach (DataGridColumn DGC in this.Columns)
                {
                    FrameworkElementFactory Factory = new FrameworkElementFactory(typeof(StackPanel));
                    FrameworkElementFactory LFactory = new FrameworkElementFactory(typeof(Label));
                    LFactory.SetValue(Label.ContentProperty, DGC.Header.ToString());
                    Factory.AppendChild(LFactory);
                    if (DGC.GetType().Name == "DataGridTextColumn")
                    {
                        FrameworkElementFactory TFactory = new FrameworkElementFactory(typeof(TextBox));
                        TFactory.SetValue(TextBox.MarginProperty, new Thickness(0));
                        TFactory.SetValue(TextBox.WidthProperty, 150.00);
                        TFactory.SetValue(TextBox.NameProperty, "txt" + DGC.Header.ToString());
                        TFactory.AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(TextBox_TextChanged), false);
                        Factory.AppendChild(TFactory);
                    }
                    if (DGC.GetType().Name == "DataGridCheckBoxColumn")
                    {
                        FrameworkElementFactory TFactory = new FrameworkElementFactory(typeof(CheckBox));
                        TFactory.SetValue(CheckBox.NameProperty, "txt" + DGC.Header.ToString());
                        TFactory.AddHandler(CheckBox.ClickEvent, new RoutedEventHandler(CheckBox_Checked), false);
                        Factory.AppendChild(TFactory);
                    }
                    DataTemplate Template = new DataTemplate();
                    Template.DataType = typeof(HeaderedContentControl);
                    Template.VisualTree = Factory;
                    DGC.HeaderTemplate = Template;
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox CB = (CheckBox)sender;
            this.SearchValue = CB.IsChecked;
            ContentPresenter CP = (ContentPresenter)CB.TemplatedParent;
            DataGridColumnHeader DGCH = (DataGridColumnHeader)CP.TemplatedParent;
            DataGridColumn DGC = DGCH.Column;
            this.ColumnName = DGC.Header.ToString();
            this.Datalist.Filter = this.CustomeFilter;
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox STB = (TextBox)sender;
            this.SearchValue = STB.Text;
            ContentPresenter CP = (ContentPresenter)STB.TemplatedParent;
            DataGridColumnHeader DGCH = (DataGridColumnHeader)CP.TemplatedParent;
            DataGridColumn DGC = DGCH.Column;
            this.ColumnName = DGC.Header.ToString();
            this.Datalist.Filter = this.CustomeFilter;
        }

        private bool CustomeFilter(object item)
        {
            Type TP = item.GetType();
            PropertyInfo PI = TP.GetProperty(this.ColumnName);
            object[] obj = new object[this.SearchValue.ToString().Length];
            string values = PI.GetValue(item, obj).ToString();
            values = values.ToUpper();
            return values.StartsWith(this.SearchValue.ToString().ToUpper());
        }

        private void CreateContextmenu()
        {
            ContextMenu contextMenuStrip = new ContextMenu();
            MenuItem toolStripMenuItem1 = new MenuItem();
            toolStripMenuItem1.Header = "Copy";
            MenuItem toolStripMenuItem2 = new MenuItem();
            toolStripMenuItem2.Header = "Copy All";
            MenuItem toolStripMenuItem3 = new MenuItem();
            toolStripMenuItem3.Header = "Save as Excel";
            toolStripMenuItem1.Click += new RoutedEventHandler(this.mnuCopy_Click);
            toolStripMenuItem2.Click += new RoutedEventHandler(this.mnuCopy_ClickAll);
            toolStripMenuItem3.Click += new RoutedEventHandler(this.mnuSaveExcel_ClickAll);

            contextMenuStrip.Items.Add(toolStripMenuItem1);
            contextMenuStrip.Items.Add(toolStripMenuItem2);
            contextMenuStrip.Items.Add(toolStripMenuItem3);

            this.ContextMenu = contextMenuStrip;
        }

        private void mnuSaveExcel_ClickAll(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "export_data_.xls";
            if (saveFileDialog.ShowDialog() != true)
                return;
            /*StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
            int count = this.Columns.Count;
            for (int index = 0; index < count; ++index)
                streamWriter.Write(this.Columns[index].Name.ToString().ToUpper() + "\t");
            streamWriter.WriteLine();
            for (int index1 = 0; index1 < this.Rows.Count - 1; ++index1)
            {
                for (int index2 = 0; index2 < count; ++index2)
                {
                    if (this.Rows[index1].Cells[index2].Value != null)
                        streamWriter.Write(this.Rows[index1].Cells[index2].Value.ToString() + "\t");
                    else
                        streamWriter.Write("\t");
                }
                streamWriter.WriteLine();
            }
            streamWriter.Close();*/

            //Datatable to Datagrid
            //this.ItemsSource = dataTable.AsDataView();

            // Datagrid to Datatable
            /*DataView view = (DataView)this.ItemsSource;
            DataTable dataTable = view.Table.Clone();
            foreach (var dataRowView in view)
            {
                dataTable.ImportRow(dataRowView.Row);
            }
            var dataTableFromDataGrid = dataTable;*/

            Microsoft.Office.Interop.Excel.Application excel = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;
            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            Microsoft.Office.Interop.Excel.Range rng = null;

            // collection of DataGrid Items
            var dtExcelDataTable = My_DataTable_Extensions.DataGridtoDataTable(this);

            excel = new Microsoft.Office.Interop.Excel.Application();
            wb = excel.Workbooks.Add();
            ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
            ws.Columns.AutoFit();
            ws.Columns.EntireColumn.ColumnWidth = 25;

            // Header row
            for (int Idx = 0; Idx < dtExcelDataTable.Columns.Count; Idx++)
            {
                ws.Range["A1"].Offset[0, Idx].Value = dtExcelDataTable.Columns[Idx].ColumnName;
            }

            // Data Rows
            for (int Idx = 0; Idx < dtExcelDataTable.Rows.Count; Idx++)
            {
                ws.Range["A2"].Offset[Idx].Resize[1, dtExcelDataTable.Columns.Count].Value = dtExcelDataTable.Rows[Idx].ItemArray;
            }

            excel.Visible = true;
            wb.Activate();
        }

        private void mnuCopy_ClickAll(object sender, EventArgs e)
        {
            this.SelectAll();
            //DataObject clipboardContent = this.GetClipboardContent();
            DataObject clipboardContent = new DataObject(Clipboard.GetDataObject());
            if (clipboardContent == null)
                return;
            Clipboard.SetDataObject((object)clipboardContent);
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            //DataObject clipboardContent = this.GetClipboardContent();
            DataObject clipboardContent = (DataObject)Clipboard.GetDataObject();
            if (clipboardContent == null)
                return;
            Clipboard.SetDataObject((object)clipboardContent);
        }
    }
}
