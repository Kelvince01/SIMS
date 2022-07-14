using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class PGroupRepostiry : RepositoryBase<PGroup>, IPGroupRepostiry, IRepository<PGroup>
    {
        public PGroupRepostiry(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
