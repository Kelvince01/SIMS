using System;

namespace sdcTool.Models
{
    public class SaleClient
    {
        public DateTime SaleDt { get; set; }

        public string PrdName { get; set; }

        public string BTName { get; set; }

        public string SSName { get; set; }

        public string CLName { get; set; }

        public string SL_ST_Name { get; set; }

        public Decimal Sqty { get; set; }

        public Decimal Rqty { get; set; }

        public Decimal RPU { get; set; }

        public Decimal DiscAmtPrd { get; set; }

        public Decimal VatPrcnt { get; set; }

        public string InvoiceNo { get; set; }

        public string PayType { get; set; }

        public string CardName { get; set; }

        public Decimal Vat { get; set; }

        public Decimal ReturnedAmt { get; set; }

        public Decimal CshAmt { get; set; }

        public Decimal CrdAmt { get; set; }

        public string SalesMan { get; set; }

        public string MRName { get; set; }

        public Decimal rVatAmt { get; set; }

        public Decimal DiscAmt { get; set; }

        public Decimal NetAmt { get; set; }

        public string ShopId { get; set; }

        public string ShopName { get; set; }

        public string CounterId { get; set; }

        public Decimal PaidAmt { get; set; }

        public Decimal ChageAmt { get; set; }

        public string TSEC { get; set; }

        public string VAT_REG_NO { get; set; }

        public string SupID { get; set; }

        public string SupName { get; set; }

        public string PrvCusId { get; set; }

        public string PrvCusName { get; set; }

        public Decimal Point { get; set; }

        public Decimal TotalEarn { get; set; }

        public Decimal RedeemPoint { get; set; }

        public string CardNo { get; set; }

        public byte[] QrCode { get; set; }

        public string ShopAddress { get; set; }

        public Decimal TotalAmt { get; set; }

        public string Barcode { get; set; }

        public Decimal Discount { get; set; }

        public string SDC_URL { get; set; }

        public string SDC_Invoice { get; set; }

        public Decimal DiscPrcnt { get; set; }
    }
}
