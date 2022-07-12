using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class AttenantLogStaffRepository :
        RepositoryBase<AttenantLogStaff>,
        IAttenantLogStaffRepository,
        IRepository<AttenantLogStaff>
    {
        public AttenantLogStaffRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
