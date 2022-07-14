using System;

namespace SIMS.Models
{
    public class PGroup
    {
        public string GroupID { get; set; }

        public string GroupName { get; set; }

        public string FloorID { get; set; }

        public Decimal? VATPrcnt { get; set; }

        public Decimal? DiscPrcnt { get; set; }

        public Decimal? CostOnSale { get; set; }
    }
}
