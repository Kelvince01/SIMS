using System;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class GlobalSetupRepository :
        RepositoryBase<GlobalSetup>,
        IGlobalSetupRepository,
        IRepository<GlobalSetup>
    {
        public GlobalSetupRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public GlobalSetup GetTopSetup() => this.DbContext.GlobalSetups.Select<GlobalSetup, GlobalSetup>((Expression<Func<GlobalSetup, GlobalSetup>>)(m => m)).FirstOrDefault<GlobalSetup>();
    }
}
