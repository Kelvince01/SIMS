using System;
using SIMS.Models;

namespace SIMS.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private SIMSWebEntities dbContext;

        public UnitOfWork(IDbFactory dbFactory) => this.dbFactory = dbFactory;

        public SIMSWebEntities DbContext => this.dbContext ?? (this.dbContext = this.dbFactory.Init());

        public void Commit()
        {
            try
            {
                this.DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this.dbContext = new SIMSWebEntities();
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                this.DbContext.Dispose();
            }
            catch (Exception ex)
            {
                this.dbContext = new SIMSWebEntities();
                throw;
            }
        }
    }
}
