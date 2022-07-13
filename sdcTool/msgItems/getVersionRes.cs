namespace sdcTool.msgItems
{
    public class getVersionRes
    {
        public string cashierID { get; set; }

        public string checkCode { get; set; }

        public string code { get; set; }

        public string data { get; set; }

        public string mode { get; set; }

        public string msg { get; set; }

        public string type { get; set; }

        public class dataItems
        {
            public string currency_version { get; set; }

            public string rateCalculationMethod { get; set; }

            public string sditem_version { get; set; }

            public string taxItemVersion { get; set; }

            public string vatitem_version { get; set; }
        }
    }
}
