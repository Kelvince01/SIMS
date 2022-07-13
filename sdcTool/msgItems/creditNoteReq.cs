using System.Collections.Generic;

namespace sdcTool.msgItems
{
    public class creditNoteReq
    {
        public string cashierID { get; set; }

        public string checkCode { get; set; }

        public string data { get; set; }

        public string type { get; set; }

        public class GoodsInfoItem
        {
            public string code { get; set; }

            public string qty { get; set; }
        }

        public class dataItems
        {
            public List<creditNoteReq.GoodsInfoItem> goodsInfo { get; set; }

            public string invoiceNo { get; set; }

            public string mobile { get; set; }

            public string taskID { get; set; }
        }
    }
}
