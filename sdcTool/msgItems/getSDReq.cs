namespace sdcTool.msgItems
{
    public class getSDReq
    {
        public string cashierID { get; set; }

        public string checkCode { get; set; }

        public string data { get; set; }

        public string type { get; set; }

        public class dataItems
        {
            public string limit { get; set; }

            public string offset { get; set; }
        }
    }
}
