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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.BLL;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls
{
    /// <summary>
    /// Interaction logic for ucSettings.xaml
    /// </summary>
    public partial class ucSettings : BaseUserControl
    {
        private IGlobalSetupService _serviceGlobal;

        //private ISupplierService _serviceSupplier;
        //private IShopListService _serviceShop;
        private GlobalSetup gs;

        public ucSettings()
        {
            InitializeComponent();
            IDbFactory idbFactory = (IDbFactory)new DbFactory();
            this._serviceGlobal = (IGlobalSetupService)new GlobalSetupService(idbFactory);
            //this._serviceSupplier = (ISupplierService)new SupplierService(idbFactory);
            //this._serviceShop = (IShopListService)new ShopListService(idbFactory);
            this.SetTheme();
        }

        public event ucSettings.afterCloseClick onCloseClick;

        private void ClearAll()
        {
        }

        public delegate void afterCloseClick(object sender);

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.onCloseClick == null)
                return;
            this.onCloseClick((object)this);
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            /*try
            {
                if (this.gs == null)
                    return;
                if (this._serviceShop.GetById(this.txtShopId.Text) == null)
                {
                    int num1 = (int)MessageBox.Show("Shop Id not found in ShopList , please update shoplist before continue operation");
                }
                this.gs.IsVatAfterDiscount = this.cbIsVatAfterDiscount.Checked ? "Y" : "N";
                this.gs.IsLargeInvoice = this.cbIsLargeInvoice.Checked;
                this.gs.AttandanceRequired = this.cbRequired.Checked;
                this.gs.PerItemSalesMan = new bool?(this.cbMultiAttendance.Checked);
                this.gs.IsSupplierWiseStock = new bool?(this.cbIsSupplierStock.Checked);
                this.gs.IsHalPayEnable = new bool?(this.cbHalPayEnable.Checked);
                this.gs.IsTouchSales = new bool?(this.cbIsTouchSales.Checked);
                this.gs.EnableBargainSales = new bool?(this.cbBargainSales.Checked);
                this.gs.SDCEnable = new bool?(this.cbSDCIntegration.Checked);
                this.gs.IsIncludingVat = new bool?(this.cbIncludingVat.Checked);
                this.gs.discount = new Decimal?(0M);
                this.gs.DecimalLengeth = int.Parse(this.txtDecLength.Text);
                this.gs.PointConversionValue = new Decimal?(Decimal.Parse(this.txtPointConversionValue.Text));
                this.gs.StoreId = this.txtShopId.Text;
                this.gs.BankCode = this.txtBankCode.Text.Trim();
                this.gs.ExpenseCode = this.txtExpenseCode.Text.Trim();
                this.gs.BarcodeLength = new int?(int.Parse(this.txtBarcodeLenght.Text));
                this.gs.BarcodePrefix = this.txtBarcodePrefix.Text;
                this.gs.BarcodeTotalLength = new int?(int.Parse(this.txtTotalBarcodeLength.Text));
                this.gs.WeightLength = new int?(int.Parse(this.txtWeightLength.Text));
                this.gs.SupplierCode = this.txtSupplierCode.Text;
                this.gs.IsAccountsOn = new bool?(this.cbIsAccountsOn.Checked);
                this.gs.SalesCode = this.txtSalesCode.Text;
                this.gs.CustomerCode = this.txtCustomerCode.Text;
                this.gs.SalesReturnCode = this.txtSalesReturnCode.Text;
                this.gs.FinishedGoodCode = this.txtFinishedGoodCode.Text.Trim();
                this.gs.COGS = this.txtCOGS.Text.Trim();
                this.gs.VatPayable = this.txtVatPayableCode.Text.Trim();
                this.gs.GeneralBarcodeLength = new Decimal?(Decimal.Parse(this.txtGenearalBarcodeLength.Text));
                this.gs.HalPIn = GlobalClass.Encrypt(this.txtHALPin.Text);
                this.gs.HalUser = this.txtHALUser.Text;
                this.gs.SaleDeletePasswordReuqired = new bool?(this.cbSaleDeleteRequirePassword.Checked);
                this.gs.SaleDeletePassword = GlobalClass.Encrypt(this.txtSaleDeletePassword.Text);
                this.gs.SDC_Default_SD_CODE = this.txtSDCSDCode.Text;
                this.gs.SDC_Default_VAT_CODE = this.txtSDCVatCode.Text;
                this.gs.AllowCreditSales = new bool?(this.cbAllowCreditSales.Checked);
                if (this.gs.IsSupplierWiseStock.HasValue && !this.gs.IsSupplierWiseStock.Value)
                    this._serviceSupplier.CheckForDefaultCustomer(StaticData.DefaultSupplierId);
                this._serviceGlobal.Update(this.gs);
                this._serviceGlobal.Save();
                int num2 = (int)MessageBox.Show("Settings Changes Successfully \r\n Please Restart software to take effect");
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }*/
        }

        private void UcSettings_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.gs = this._serviceGlobal.GetTopSetup();
            if (this.gs == null)
                return;
        }
    }
}
