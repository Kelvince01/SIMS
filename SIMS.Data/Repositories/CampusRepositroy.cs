using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class CampusRepositroy
        :
            RepositoryBase<Campus>,
            ICampusRepositroy,
            IRepository<Campus>
    {
        public CampusRepositroy(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public Campus GetById(string campusId) => this.DbContext.Campuses.Where<Campus>((Expression<Func<Campus, bool>>)(m => m.CampusID == campusId)).FirstOrDefault<Campus>();

        public void RemoveAll() => this.DbContext.Campuses.RemoveRange((IEnumerable<Campus>)this.DbContext.Campuses);
    }
}
