using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class DesktopMenuRepository :
        RepositoryBase<DesktopMenu>,
        IDesktopMenuRepository,
        IRepository<DesktopMenu>
    {
        public DesktopMenuRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
