using System.Collections.Generic;

namespace sdcTool.msgItems
{
    public class invoiceReq
    {
        public string cashierID { get; set; }

        public string checkCode { get; set; }

        public string data { get; set; }

        public string type { get; set; }

        public class GoodsInfoItem
        {
            public string code { get; set; }

            public string hsCode { get; set; }

            public string item { get; set; }

            public string price { get; set; }

            public string qty { get; set; }

            public string sd_category { get; set; }

            public string vat_category { get; set; }
        }

        public class dataItems
        {
            public string buyerInfo { get; set; }

            public string currency_code { get; set; }

            public List<invoiceReq.GoodsInfoItem> goodsInfo { get; set; }

            public string payType { get; set; }

            public string taskID { get; set; }
        }
    }
}
