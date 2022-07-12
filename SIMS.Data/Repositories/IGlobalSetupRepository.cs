using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public interface IGlobalSetupRepository : IRepository<GlobalSetup>
    {
        GlobalSetup GetTopSetup();
    }
}
