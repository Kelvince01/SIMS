using System.Collections.Generic;

namespace sdcTool.msgItems
{
    public class getSDRes
    {
        public string cashierID { get; set; }

        public string checkCode { get; set; }

        public string code { get; set; }

        public string data { get; set; }

        public string mode { get; set; }

        public string msg { get; set; }

        public string type { get; set; }

        public class RatesItem
        {
            public string id { get; set; }

            public string slNo { get; set; }

            public string sdRate { get; set; }

            public string serviceName { get; set; }

            public string serviceCode { get; set; }

            public string sdType { get; set; }

            public string effectFrom { get; set; }
        }

        public class dataItems
        {
            public string payType { get; set; }

            public List<getSDRes.RatesItem> rates { get; set; }

            public string vat_item_version { get; set; }

            public string total { get; set; }
        }
    }
}
