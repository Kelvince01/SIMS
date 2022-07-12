using System;

namespace SIMS.Models
{
    public class GlobalSetup
    {
        public string GlobalSetupID { get; set; }

        public Decimal? goldrate { get; set; }

        public Decimal? discount { get; set; }

        public string StoreId { get; set; }

        public Decimal? VAT { get; set; }

        public string ALLOWBNL { get; set; }

        public string TAP { get; set; }

        public Decimal? VATL { get; set; }

        public string EnableSCAN { get; set; }

        public string ShowPOL { get; set; }

        public string AllowMultiScan { get; set; }

        public string OpenCashDrawer { get; set; }

        public string MaxOrderCheck { get; set; }

        public string IsZoneID { get; set; }

        public string IsPoint { get; set; }

        public DateTime? FiscYr { get; set; }

        public DateTime? FiscYr2 { get; set; }

        public string IsCPUAverage { get; set; }

        public string DisableWSP { get; set; }

        public string IsVatAfterDiscount { get; set; }

        public string BankCode { get; set; }

        public string ExpenseCode { get; set; }

        public bool IsLargeInvoice { get; set; }

        public int DecimalLengeth { get; set; }

        public bool AttandanceRequired { get; set; }

        public bool? IsSupplierWiseStock { get; set; }

        public int? BarcodeLength { get; set; }

        public string BarcodePrefix { get; set; }

        public int? BarcodeTotalLength { get; set; }

        public int? WeightLength { get; set; }

        public Decimal? PointConversionValue { get; set; }

        public bool? PerItemSalesMan { get; set; }

        public bool? SaleDeletePasswordReuqired { get; set; }

        public string SaleDeletePassword { get; set; }

        public bool? IsTouchSales { get; set; }

        public bool? IsAccountsOn { get; set; }

        public string SupplierCode { get; set; }

        public bool? IsIncludingVat { get; set; }

        public string SalesCode { get; set; }

        public string SalesReturnCode { get; set; }

        public Decimal? GeneralBarcodeLength { get; set; }

        public string FinishedGoodCode { get; set; }

        public string SalesCodeCard { get; set; }

        public string COGS { get; set; }

        public string VatPayable { get; set; }

        public string CustomerCode { get; set; }

        public bool? IsHalPayEnable { get; set; }

        public string HalUser { get; set; }

        public string HalPIn { get; set; }

        public bool? AllowCreditSales { get; set; }

        public bool? EnableBargainSales { get; set; }

        public bool? SDCEnable { get; set; }

        public string SDC_Default_VAT_CODE { get; set; }

        public string SDC_Default_SD_CODE { get; set; }
    }
}
