using System;
using SIMS.Models;

namespace SIMS.Data.Infrastructure
{
    public class DbFactory : IDbFactory, IDisposable
    {
        private SIMSWebEntities dbContext;

        public SIMSWebEntities Init() => this.dbContext ?? (this.dbContext = new SIMSWebEntities());

        public void Dispose()
        {
            if (this.dbContext == null)
                return;
            this.dbContext.Dispose();
        }
    }
}
