using System;

namespace SIMS.Models
{
    public class Act_MasterChartOfAccount
    {
        public Decimal ChartID { get; set; }

        public Decimal CompanyID { get; set; }

        public Decimal ActCode { get; set; }

        public string ActName { get; set; }

        public int Level { get; set; }

        public Decimal? ParentID { get; set; }

        public Decimal? ID { get; set; }

        public int? FiscalYear { get; set; }

        public string IsTransactional { get; set; }

        public string IsControl { get; set; }
    }
}
