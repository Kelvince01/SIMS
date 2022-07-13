using System.Collections.Generic;

namespace sdcTool.msgItems
{
    public class invoiceRes
    {
        public string cashierID { get; set; }

        public string checkCode { get; set; }

        public string code { get; set; }

        public string data { get; set; }

        public string mode { get; set; }

        public string msg { get; set; }

        public string type { get; set; }

        public class GoodsInfoItem
        {
            public string itemCode { get; set; }

            public string item_name { get; set; }

            public string noTaxAmt { get; set; }

            public string oriPrice { get; set; }

            public string qty { get; set; }

            public string rateType { get; set; }

            public string sdAmt { get; set; }

            public string supplementary_duty { get; set; }

            public string taxAmt { get; set; }

            public string tax_due { get; set; }

            public string unit_price { get; set; }
        }

        public class dataItems
        {
            public string amount { get; set; }

            public string approvalCode { get; set; }

            public string bin { get; set; }

            public string client_invoice_datetime { get; set; }

            public List<invoiceRes.GoodsInfoItem> goodsInfo { get; set; }

            public string invoiceCount { get; set; }

            public string invoiceNo { get; set; }

            public string payType { get; set; }

            public string qrCode { get; set; }

            public string rateDetAmt { get; set; }

            public string rateDetV { get; set; }

            public string sdAmount { get; set; }

            public string seller_addr { get; set; }

            public string seller_name { get; set; }

            public string taxAmount { get; set; }

            public string terminal { get; set; }

            public string totalAmount { get; set; }
        }
    }
}
