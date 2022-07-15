using System;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class DesignationRepository
        :
            RepositoryBase<Designation>,
            IDesignationRepository,
            IRepository<Designation>
    {
        public DesignationRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public override void Add(Designation entity)
        {
            Designation designation = this.DbContext.Designations.Where<Designation>((Expression<Func<Designation, bool>>)(m => m.DesignationId == entity.DesignationId)).FirstOrDefault<Designation>();
            if (designation != null)
            {
                designation.DesignationName = entity.DesignationName;
                designation.SortOrder = entity.SortOrder;
            }
            else
            {
                entity.DesignationId = -1;
                base.Add(entity);
            }
        }
    }
}
