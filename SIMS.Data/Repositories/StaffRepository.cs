using System;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class StaffRepository :
            RepositoryBase<Staff>,
            IStaffRepository,
            IRepository<Staff>
    {
        public StaffRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public override void Add(Staff entity)
        {
            Staff staff = this.DbContext.Staffs.Where<Staff>((Expression<Func<Staff, bool>>)(m => m.StaffId == entity.StaffId)).FirstOrDefault<Staff>();
            if (staff != null)
            {
                staff.Address = entity.Address;
                staff.CardNo = entity.CardNo;
                staff.DesignationId = entity.DesignationId;
                staff.Email = entity.Email;
                staff.StaffName = entity.StaffName;
                staff.Mobile = entity.Mobile;
                staff.CampusId = entity.CampusId;
                staff.IsActive = entity.IsActive;
            }
            else
                base.Add(entity);
        }
    }
}
