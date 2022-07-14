using System;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class DiscriminatorRepository
        :
            RepositoryBase<Discriminator>,
            IDiscriminatorRepository,
            IRepository<Discriminator>
    {
        public DiscriminatorRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public override void Add(Discriminator entity)
        {
            Discriminator designation = this.DbContext.Discriminators.Where<Discriminator>((Expression<Func<Discriminator, bool>>)(m => m.DiscriminatorId == entity.DiscriminatorId)).FirstOrDefault<Discriminator>();
            if (designation != null)
            {
                designation.DiscriminatorName = entity.DiscriminatorName;
                designation.SortOrder = entity.SortOrder;
            }
            else
            {
                entity.DiscriminatorId = -1;
                base.Add(entity);
            }
        }
    }
}
