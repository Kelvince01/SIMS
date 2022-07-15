using System;

namespace SIMS.Reports.Class
{
    public class SP_ReportStaffAttendance
    {
        public string CampusId { get; set; }

        public string CampusName { get; set; }

        public string STaffCode { get; set; }

        public string StaffName { get; set; }

        public string Designation { get; set; }

        public DateTime MonthDates { get; set; }

        public string DaysName { get; set; }

        public string Statuss { get; set; }

        public DateTime IN_TIME { get; set; }

        public DateTime OUT_TIME { get; set; }

        public DateTime LATE_HOUR { get; set; }

        public DateTime OT_HOUR { get; set; }
    }
}
