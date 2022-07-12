using System;

namespace SIMS.Models
{
    public class AttenantLogStaff
    {
        public Decimal AttendentId { get; set; }

        public string ShopID { get; set; }

        public DateTime? InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public string IsTransfer { get; set; }

        public int EmployeeId { get; set; }

        public string CardNo { get; set; }

        public string EmpName { get; set; }

        public string ShopName { get; set; }

        public int? DesignationId { get; set; }
    }
}
