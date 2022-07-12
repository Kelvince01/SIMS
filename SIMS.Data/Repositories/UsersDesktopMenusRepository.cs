using System;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class UsersDesktopMenusRepository :
        RepositoryBase<UsersDesktopMenu>,
        IUsersDesktopMenusRepository,
        IRepository<UsersDesktopMenu>
    {
        public UsersDesktopMenusRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public override void Add(UsersDesktopMenu entity)
        {
            UsersDesktopMenu entity1 = this.DbContext.UsersDesktopMenus.Where<UsersDesktopMenu>((Expression<Func<UsersDesktopMenu, bool>>)(m => m.UserName == entity.UserName)).FirstOrDefault<UsersDesktopMenu>();
            if (entity1 != null)
                this.Delete(entity1);
            base.Add(entity);
        }
    }
}
