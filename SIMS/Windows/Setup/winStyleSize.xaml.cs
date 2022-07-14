using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CustomControls;
using SIMS.BLL;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.Windows.Setup
{
    /// <summary>
    /// Interaction logic for winStyleSize.xaml
    /// </summary>
    public partial class winStyleSize : Window
    {
        private IStyleSizeService _service;

        //private IBuyService _serviceBuy;
        public string sbarocde = "";

        private bool isCPU = false;
        private bool isRPU = false;
        private bool isProfit = false;
        private Window win;
        //private ucProductInformation uc;

        public winStyleSize()
        {
            InitializeComponent();
            IDbFactory idbFactory = (IDbFactory)new DbFactory();
            //this._serviceBuy = (IBuyService)new BuyService(idbFactory);
            this._service = (IStyleSizeService)new StyleSizeService(idbFactory);
            /*if (StaticData.globalSetup.IsSupplierWiseStock.Value)
            {
                this.lblSupplier.Visible = true;
                this.cmbSupplier.Visible = true;
                this.btnSuppliers.Visible = true;
            }
            else
            {
                this.lblSupplier.Visible = false;
                this.cmbSupplier.Visible = false;
                this.btnSuppliers.Visible = false;
            }*/
        }

        private void WinStyleSize_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.BindComboBox();
            /*this.btnDelete.Enabled = false;
            this.txtSDCSDCode.Text = StaticData.globalSetup.SDC_Default_SD_CODE;
            this.txtSDCVatCode.Text = StaticData.globalSetup.SDC_Default_VAT_CODE;
            if (this.sbarocde != "")
            {
                this.btnDelete.Enabled = true;
                this.LoadSavedData();
            }
            this.panel1.Location = new Point(this.panel1.Location.X, this.Height / 6);
            bool? sdcEnable = StaticData.globalSetup.SDCEnable;
            int num;
            if (sdcEnable.HasValue)
            {
                sdcEnable = StaticData.globalSetup.SDCEnable;
                num = !sdcEnable.Value ? 1 : 0;
            }
            else
                num = 1;
            if (num != 0)
                return;
            this.lblSDCSD.Visible = true;
            this.lblSDCVat.Visible = true;
            this.txtSDCSDCode.Visible = true;
            this.txtSDCVatCode.Visible = true;
            this.btnSDC.Visible = true;*/
        }

        private void LoadSavedData()
        {
            this.ClearAll();
            StyleSize styleSize = new StyleSizeService((IDbFactory)new DbFactory()).Get(this.sbarocde);
            if (styleSize == null)
                return;
            this.isCPU = true;
            this.isRPU = true;
            this.isProfit = true;
            /*this.txtZoneId.Text = styleSize.ZoneID;
            NumericTextBox txtWsq = this.txtWSQ;
            Decimal? nullable1;
            string str1;
            if (!styleSize.WSQ.HasValue)
            {
                str1 = "0";
            }
            else
            {
                nullable1 = styleSize.WSQ;
                str1 = nullable1.ToString();
            }
            txtWsq.Text = str1;
            NumericTextBox txtWsp = this.txtWSP;
            nullable1 = styleSize.WSP;
            string str2;
            if (!nullable1.HasValue)
            {
                str2 = "0";
            }
            else
            {
                nullable1 = styleSize.WSP;
                str2 = nullable1.ToString();
            }
            txtWsp.Text = str2;
            NumericTextBox txtVatPrcnt = this.txtVatPrcnt;
            nullable1 = styleSize.VATPrcnt;
            string str3;
            if (!nullable1.HasValue)
            {
                str3 = "0";
            }
            else
            {
                nullable1 = styleSize.VATPrcnt;
                str3 = nullable1.ToString();
            }
            txtVatPrcnt.Text = str3;
            this.txtStyleSize.Text = styleSize.SSName;
            this.txtSBarcode.Text = styleSize.sBarcode;
            NumericTextBox txtRpu = this.txtRPU;
            nullable1 = styleSize.RPU;
            string str4;
            if (!nullable1.HasValue)
            {
                str4 = "0";
            }
            else
            {
                nullable1 = styleSize.RPU;
                str4 = nullable1.ToString();
            }
            txtRpu.Text = str4;
            NumericTextBox txtRpp = this.txtRPP;
            nullable1 = styleSize.RPP;
            string str5;
            if (!nullable1.HasValue)
            {
                str5 = "0";
            }
            else
            {
                nullable1 = styleSize.RPP;
                str5 = nullable1.ToString();
            }
            txtRpp.Text = str5;
            NumericTextBox txtPoint = this.txtPoint;
            nullable1 = styleSize.Point;
            string str6;
            if (!nullable1.HasValue)
            {
                str6 = "0";
            }
            else
            {
                nullable1 = styleSize.Point;
                str6 = nullable1.ToString();
            }
            txtPoint.Text = str6;
            NumericTextBox txtMinOrderQty = this.txtMinOrderQty;
            nullable1 = styleSize.MinOrder;
            string str7;
            if (!nullable1.HasValue)
            {
                str7 = "0";
            }
            else
            {
                nullable1 = styleSize.MinOrder;
                str7 = nullable1.ToString();
            }
            txtMinOrderQty.Text = str7;
            NumericTextBox txtReOrderQty = this.txtReOrderQty;
            nullable1 = styleSize.Reorder;
            string str8;
            if (!nullable1.HasValue)
            {
                str8 = "0";
            }
            else
            {
                nullable1 = styleSize.Reorder;
                str8 = nullable1.ToString();
            }
            txtReOrderQty.Text = str8;
            NumericTextBox txtMaxOrderQty = this.txtMaxOrderQty;
            nullable1 = styleSize.MaxOrder;
            string str9;
            if (!nullable1.HasValue)
            {
                str9 = "0";
            }
            else
            {
                nullable1 = styleSize.MaxOrder;
                str9 = nullable1.ToString();
            }
            txtMaxOrderQty.Text = str9;
            NumericTextBox txtDiscPrcnt = this.txtDiscPrcnt;
            nullable1 = styleSize.DiscPrcnt;
            string str10;
            if (!nullable1.HasValue)
            {
                str10 = "0";
            }
            else
            {
                nullable1 = styleSize.DiscPrcnt;
                str10 = nullable1.ToString();
            }
            txtDiscPrcnt.Text = str10;
            NumericTextBox txtCpu = this.txtCPU;
            nullable1 = styleSize.CPU;
            string str11;
            if (!nullable1.HasValue)
            {
                str11 = "0";
            }
            else
            {
                nullable1 = styleSize.CPU;
                str11 = nullable1.ToString();
            }
            txtCpu.Text = str11;
            this.cbDiscontued.Checked = styleSize.DisContinued == "1";
            this.cbNegativeSales.Checked = styleSize.IsNegativeStockSale.HasValue && styleSize.IsNegativeStockSale.Value;
            CheckBox cbIsWeiging = this.cbIsWeiging;
            bool? nullable2 = styleSize.IsWeighing;
            int num1;
            if (!nullable2.HasValue)
            {
                num1 = 0;
            }
            else
            {
                nullable2 = styleSize.IsWeighing;
                num1 = nullable2.Value ? 1 : 0;
            }
            cbIsWeiging.Checked = num1 != 0;
            CheckBox cbIsPc = this.cbIsPc;
            nullable2 = styleSize.IsPcs;
            int num2;
            if (!nullable2.HasValue)
            {
                num2 = 0;
            }
            else
            {
                nullable2 = styleSize.IsPcs;
                num2 = nullable2.Value ? 1 : 0;
            }
            cbIsPc.Checked = num2 != 0;
            if (this.cbIsWeiging.Checked)
                this.cbIsPc.Visible = true;
            this.txtSDCVatCode.Text = styleSize.SDC_VAT_CODE;
            this.txtSDCSDCode.Text = styleSize.SDC_SD_CODE;
            this.txtBarcode.Text = styleSize.Barcode;
            this.cmbSupplier.SelectedValue = (object)styleSize.SupID;
            this.cmbGroup.SelectedValue = (object)styleSize.GroupID;
            this.cmbGroup_SelectedIndexChanged((object)null, (EventArgs)null);
            this.cmbProduct.SelectedValue = (object)styleSize.PrdID;
            this.cmbBrand.SelectedValue = (object)styleSize.BTID;
            this.cmbBrand.Enabled = this.cmbGroup.Enabled = this.cmbProduct.Enabled = this.cmbSupplier.Enabled = false;
            this.txtSBarcode.Enabled = this.txtBarcode.Enabled = false;
            this.btnBrand.Enabled = this.btnGroup.Enabled = this.btnProduct.Enabled = this.btnSuppliers.Enabled = false;
            this.CalculateGP();
            this.isCPU = false;
            this.isRPU = false;
            this.isProfit = false;*/
        }

        private void BindComboBox(string suggestion = null)
        {
            /*if (suggestion == null || suggestion == "sup")
            {
                List<Supplier> list = new SupplierService((IDbFactory)new DbFactory()).Gets((string)null).ToList<Supplier>();
                if (StaticData.globalSetup.IsSupplierWiseStock.Value)
                    list = Enumerable.Where<Supplier>((IEnumerable<Supplier>)list, (Func<Supplier, bool>)(m => m.SupID != StaticData.DefaultSupplierId)).ToList<Supplier>();
                list.Insert(0, new Supplier()
                {
                    SupID = "",
                    Supname = "Select"
                });
                this.cmbSupplier.DataSource = (object)list;
                this.cmbSupplier.DisplayMember = "Supname";
                this.cmbSupplier.ValueMember = "SupID";
            }
            if (suggestion == null || suggestion == "group")
            {
                List<PGroup> list = new PGroupService((IDbFactory)new DbFactory()).Gets((string)null).ToList<PGroup>();
                list.Insert(0, new PGroup()
                {
                    GroupID = "",
                    GroupName = "Select"
                });
                this.cmbGroup.DataSource = (object)list;
                this.cmbGroup.DisplayMember = "GroupName";
                this.cmbGroup.ValueMember = "GroupID";
            }
            if (suggestion != null && !(suggestion == "brand"))
                return;
            List<BrandType> list1 = new BrandTypeService((IDbFactory)new DbFactory()).Gets((string)null).ToList<BrandType>();
            list1.Insert(0, new BrandType()
            {
                BTID = "",
                BTName = "Select"
            });
            this.cmbBrand.DataSource = (object)list1;
            this.cmbBrand.DisplayMember = "BTName";
            this.cmbBrand.ValueMember = "BTID";*/
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            /*try
            {
                if (this.txtStyleSize.Text == "")
                {
                    int num = (int)MessageBox.Show("Enter Style name");
                    this.txtStyleSize.Focus();
                }
                else if (this.cmbBrand.SelectedIndex == 0)
                {
                    int num = (int)MessageBox.Show("Select Brand name");
                    this.cmbBrand.Focus();
                }
                else if (StaticData.globalSetup.IsSupplierWiseStock.Value && this.cmbSupplier.SelectedIndex == 0)
                {
                    int num = (int)MessageBox.Show("Select supplier name");
                    this.cmbSupplier.Focus();
                }
                else if (this.cmbProduct.SelectedIndex == 0)
                {
                    int num = (int)MessageBox.Show("Select Product name");
                    this.cmbProduct.Focus();
                }
                else if (this.cmbGroup.SelectedIndex == 0)
                {
                    int num = (int)MessageBox.Show("Select Group name");
                    this.cmbGroup.Focus();
                }
                else
                {
                    Decimal num1 = Decimal.Parse(this.txtCPU.Text);
                    Decimal num2 = Decimal.Parse(this.txtRPU.Text);
                    if (num2 < num1)
                    {
                        int num3 = (int)MessageBox.Show("RPU must be greater then cpu");
                    }
                    else
                    {
                        if ((num1 == 0M || num2 == 0M) && MessageBox.Show((IWin32Window)this, "CPU / RPU is Zero do you want to save ?", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                            return;
                        if (this.cbIsWeiging.Checked)
                        {
                            int length = this.txtBarcode.Text.Length;
                            int? barcodeLength = StaticData.globalSetup.BarcodeLength;
                            if ((length != barcodeLength.GetValueOrDefault() ? 1 : (!barcodeLength.HasValue ? 1 : 0)) != 0)
                            {
                                int num4 = (int)MessageBox.Show("Barcode lenght is invalid for Weiging item");
                                return;
                            }
                        }
                        bool? nullable = StaticData.globalSetup.SDCEnable;
                        int num5;
                        if (nullable.HasValue)
                        {
                            nullable = StaticData.globalSetup.SDCEnable;
                            num5 = !nullable.Value ? 1 : 0;
                        }
                        else
                            num5 = 1;
                        if (num5 == 0)
                        {
                            if (this.txtSDCVatCode.Text == "")
                            {
                                int num6 = (int)MessageBox.Show("VAT Code Required when SDC is enable");
                                return;
                            }
                            if (this.txtSDCSDCode.Text == "")
                            {
                                int num7 = (int)MessageBox.Show("SD Code Required when SDC is enable");
                                return;
                            }
                        }
                        StyleSize model = new StyleSize();
                        if (!string.IsNullOrEmpty(this.sbarocde))
                            model = this._service.Get(this.sbarocde);
                        model.ZoneID = this.txtZoneId.Text;
                        model.WSQ = new Decimal?(Decimal.Parse(this.txtWSQ.Text));
                        model.WSP = new Decimal?(Decimal.Parse(this.txtWSP.Text));
                        model.VATPrcnt = new Decimal?(Decimal.Parse(this.txtVatPrcnt.Text));
                        model.SSName = this.txtStyleSize.Text;
                        model.sBarcode = this.txtSBarcode.Text;
                        model.RPU = new Decimal?(Decimal.Parse(this.txtRPU.Text));
                        model.Point = new Decimal?(Decimal.Parse(this.txtPoint.Text));
                        model.Reorder = new Decimal?(Decimal.Parse(this.txtReOrderQty.Text));
                        model.MinOrder = new Decimal?(Decimal.Parse(this.txtMinOrderQty.Text));
                        model.MaxOrder = new Decimal?(Decimal.Parse(this.txtMaxOrderQty.Text));
                        model.DiscPrcnt = new Decimal?(Decimal.Parse(this.txtDiscPrcnt.Text));
                        model.CPU = new Decimal?(Decimal.Parse(this.txtCPU.Text));
                        model.RPP = new Decimal?(Decimal.Parse(this.txtRPP.Text));
                        model.Barcode = this.txtBarcode.Text;
                        model.SupID = this.cmbSupplier.SelectedValue.ToString();
                        nullable = StaticData.globalSetup.IsSupplierWiseStock;
                        if (!nullable.Value)
                            model.SupID = StaticData.DefaultSupplierId;
                        model.SDC_SD_CODE = this.txtSDCSDCode.Text;
                        model.SDC_VAT_CODE = this.txtSDCVatCode.Text;
                        model.IsWeighing = new bool?(this.cbIsWeiging.Checked);
                        model.IsPcs = new bool?(this.cbIsPc.Checked);
                        model.GroupID = this.cmbGroup.SelectedValue.ToString();
                        model.PrdID = this.cmbProduct.SelectedValue.ToString();
                        model.BTID = this.cmbBrand.SelectedValue.ToString();
                        model.CBTID = model.PrdID + model.BTID;
                        model.SSID = this.txtSBarcode.Text.Substring(15, 3);
                        model.DisContinued = this.cbDiscontued.Checked ? "1" : "0";
                        model.IsNegativeStockSale = new bool?(this.cbNegativeSales.Checked);
                        Product byId = new ProductService((IDbFactory)new DbFactory()).GetById(model.PrdID);
                        model.FloorID = byId.FloorID;
                        model.UserID = StaticData.UserId;
                        model.ENTRYDT = new DateTime?(DateTime.Now.Date);
                        if (!string.IsNullOrEmpty(this.sbarocde))
                        {
                            model.sBarcode = this.sbarocde;
                            this._service.Update(model);
                            this._serviceBuy.UpdateFromSS(model);
                            this._service.Save();
                        }
                        else
                        {
                            if (this._service.GetByBarcode(this.txtBarcode.Text) != null)
                            {
                                if (MessageBox.Show(this, "This Barcode already exists, do you want to generate new one", "Duplicate barcode", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                    return;
                                this.GenerateBarcode();
                            }
                            this.GenerateSBarcode();
                            model.sBarcode = this.txtSBarcode.Text;
                            model.Barcode = this.txtBarcode.Text;
                            model.CMPIDX = this.txtSBarcode.Text + this.txtBarcode.Text;
                            string maxId = new GlobalClass().GetMaxId("NewsBarcode", "StyleSize");
                            model.NewsBarcode = maxId;
                            this._service.Create(model);
                            this._service.Save();
                            this.cmbProduct.Focus();
                        }
                        int num8 = (int)MessageBox.Show("Save Successfully");
                        this.ClearAll();
                    }
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }*/
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to remove this record", "Delete confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
                    return;
                if (!string.IsNullOrEmpty(this.sbarocde))
                {
                    this._service.Remove(this._service.Get(this.sbarocde));
                    this._service.Save();
                    int num = (int)MessageBox.Show("Delete Successfully");
                    this.ClearAll();
                    this.Close();
                }
                else
                {
                    int num1 = (int)MessageBox.Show("No record for delete");
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearAll()
        {
            /*this.txtBarcode.Text = this.txtCPU.Text = this.txtDiscPrcnt.Text = this.txtMaxOrderQty.Text = this.txtMinOrderQty.Text = this.txtReOrderQty.Text = this.txtPoint.Text = "";
            this.cbIsWeiging.Checked = false;
            this.cbIsPc.Checked = false;
            this.txtRPP.Text = this.txtRPU.Text = this.txtSBarcode.Text = this.txtStyleSize.Text = this.txtVatPrcnt.Text = this.txtWSP.Text = "";
            this.txtWSQ.Text = this.txtZoneId.Text = "";
            this.cmbProduct.SelectedIndex = -1;
            this.cmbBrand.Enabled = this.cmbGroup.Enabled = this.cmbProduct.Enabled = this.cmbSupplier.Enabled = true;
            this.btnBrand.Enabled = this.btnGroup.Enabled = this.btnProduct.Enabled = this.btnSuppliers.Enabled = true;
            this.txtSDCSDCode.Text = StaticData.globalSetup.SDC_Default_SD_CODE;
            this.txtSDCVatCode.Text = StaticData.globalSetup.SDC_Default_VAT_CODE;*/
        }
    }
}
