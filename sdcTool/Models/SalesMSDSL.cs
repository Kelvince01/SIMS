using System;

namespace sdcTool.Models
{
    public class SalesMSDSL
    {
        public string BarCode { get; set; }

        public Decimal sQty { get; set; }

        public Decimal rQty { get; set; }

        public Decimal RPU { get; set; }

        public Decimal VATPrcnt { get; set; }

        public string Product { get; set; }

        public string Invoice { get; set; }

        public string CInvoice { get; set; }

        public string SD_Code { get; set; }

        public string Vat_Code { get; set; }
    }
}
