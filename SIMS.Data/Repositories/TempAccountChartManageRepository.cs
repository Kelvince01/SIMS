using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class TempAccountChartManageRepository :
        RepositoryBase<TempAccountChartManage>,
        ITempAccountChartManageRepository,
        IRepository<TempAccountChartManage>
    {
        public TempAccountChartManageRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
