namespace SIMS.Models
{
    public class Staff
    {
        public int StaffId { get; set; }

        public string CardNo { get; set; }

        public string StaffName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int? DesignationId { get; set; }

        public string CampusId { get; set; }

        public string IsActive { get; set; }
    }
}
