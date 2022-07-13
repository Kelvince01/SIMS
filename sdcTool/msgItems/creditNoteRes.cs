using System.Collections.Generic;

namespace sdcTool.msgItems
{
    public class creditNoteRes
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
            public string item { get; set; }

            public string product_code { get; set; }

            public string qty { get; set; }

            public string rateType { get; set; }

            public string sdAmt { get; set; }

            public string sd_category { get; set; }

            public string sd_value { get; set; }

            public string unitPrice { get; set; }

            public string vatAmt { get; set; }

            public string vat_category { get; set; }

            public string vat_value { get; set; }
        }

        public class dataItems
        {
            public string amount { get; set; }

            public string bin { get; set; }

            public string client_invoice_datetime { get; set; }

            public List<creditNoteRes.GoodsInfoItem> goodsInfo { get; set; }

            public string invoiceNo { get; set; }

            public string mobile { get; set; }

            public string sdAmount { get; set; }

            public string seller_addr { get; set; }

            public string seller_name { get; set; }

            public string taxAmount { get; set; }

            public string terminal { get; set; }
        }
    }
}
