using System;

namespace SIMS.Models
{
    public class StyleSize
    {
        public Decimal OldRPU { get; set; }

        public Decimal BalQty { get; set; }

        public string CMPIDX { get; set; }

        public string sBarcode { get; set; }

        public string Barcode { get; set; }

        public string NewsBarcode { get; set; }

        public string CSSID { get; set; }

        public string SSID { get; set; }

        public string SSName { get; set; }

        public string PrdID { get; set; }

        public string PrdName { get; set; }

        public string CBTID { get; set; }

        public string BTID { get; set; }

        public string BTName { get; set; }

        public string GroupID { get; set; }

        public string GroupName { get; set; }

        public string FloorID { get; set; }

        public Decimal? DiscPrcnt { get; set; }

        public Decimal? VATPrcnt { get; set; }

        public Decimal? PrdComm { get; set; }

        public Decimal? CPU { get; set; }

        public Decimal? RPU { get; set; }

        public Decimal? RPP { get; set; }

        public Decimal? WSP { get; set; }

        public Decimal? WSQ { get; set; }

        public string DisContinued { get; set; }

        public string SupID { get; set; }

        public string SupName { get; set; }

        public string UserID { get; set; }

        public DateTime? ENTRYDT { get; set; }

        public string ZoneID { get; set; }

        public Decimal? Point { get; set; }

        public Decimal? Reorder { get; set; }

        public Decimal? MinOrder { get; set; }

        public Decimal? MaxOrder { get; set; }

        public bool? IsNegativeStockSale { get; set; }

        public bool? IsWeighing { get; set; }

        public bool? IsPcs { get; set; }

        public Decimal AutoIdForBarcode { get; set; }

        public string SDC_SD_CODE { get; set; }

        public string SDC_VAT_CODE { get; set; }
    }
}
