using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class AttenantLogRepository :
        RepositoryBase<AttenantLog>,
        IAttenantLogRepository,
        IRepository<AttenantLog>
    {
        public AttenantLogRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public override void Add(AttenantLog entity)
        {
            AttenantLog attenantLog = this.DbContext.AttenantLogs.Where<AttenantLog>((Expression<Func<AttenantLog, bool>>)(m => m.ShopID == entity.ShopID && DbFunctions.TruncateTime(m.InTime) == DbFunctions.TruncateTime(entity.InTime))).FirstOrDefault<AttenantLog>();
            if (attenantLog != null)
            {
                attenantLog.OutTime = entity.OutTime;
                attenantLog.IsTransfer = "N";
            }
            else
                base.Add(entity);
        }
    }
}
