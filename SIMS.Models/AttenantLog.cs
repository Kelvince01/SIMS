using System;

namespace SIMS.Models
{
    public class AttenantLog
    {
        public Decimal AttendentId { get; set; }

        public string ShopID { get; set; }

        public DateTime? InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public string IsTransfer { get; set; }
    }
}
