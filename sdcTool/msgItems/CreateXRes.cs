using System;
using System.Collections.Generic;

namespace sdcTool.msgItems
{
    internal class CreateXRes
    {
        public string cashierID { get; set; }

        public string checkCode { get; set; }

        public string code { get; set; }

        public string data { get; set; }

        public string mode { get; set; }

        public string msg { get; set; }

        public string type { get; set; }

        public List<CreateXRes.dataItems> list { get; set; }

        public int total { get; set; }

        public class dataItems
        {
            public string amount { get; set; }

            public string bin { get; set; }

            public string cardNo { get; set; }

            public string cashNo { get; set; }

            public string cashier_name { get; set; }

            public string creditNo { get; set; }

            public string date { get; set; }

            public string isPrint { get; set; }

            public string isUp { get; set; }

            public string report_count { get; set; }

            public string sdAmount { get; set; }

            public string seller_address { get; set; }

            public string seller_name { get; set; }

            public string session_end_time { get; set; }

            public string session_start_time { get; set; }

            public string sessionid { get; set; }

            public string sn { get; set; }

            public string tax_amount { get; set; }

            public string totalAmount { get; set; }

            public string total_invoice_number { get; set; }

            public string void_amount { get; set; }

            public string void_sdAmount { get; set; }

            public string void_tax_amount { get; set; }

            public string void_totalAmount { get; set; }
        }

        public class dataItemsHolder
        {
        }
    }
}
