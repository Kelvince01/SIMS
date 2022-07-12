using System;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class Act_MasterChartOfAccountRepository :
        RepositoryBase<Act_MasterChartOfAccount>,
        IAct_MasterChartOfAccountRepository,
        IRepository<Act_MasterChartOfAccount>
    {
        public Act_MasterChartOfAccountRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public override void Add(Act_MasterChartOfAccount entity)
        {
            Act_MasterChartOfAccount masterChartOfAccount = this.DbContext.Act_MasterChartOfAccount.Where<Act_MasterChartOfAccount>((Expression<Func<Act_MasterChartOfAccount, bool>>)(m => m.ChartID == entity.ChartID)).FirstOrDefault<Act_MasterChartOfAccount>();
            if (masterChartOfAccount == null)
            {
                base.Add(entity);
            }
            else
            {
                masterChartOfAccount.ActName = entity.ActName;
                masterChartOfAccount.FiscalYear = entity.FiscalYear;
                masterChartOfAccount.IsControl = entity.IsControl;
                masterChartOfAccount.IsTransactional = entity.IsTransactional;
                masterChartOfAccount.Level = entity.Level;
                masterChartOfAccount.ParentID = entity.ParentID;
            }
        }
    }
}
