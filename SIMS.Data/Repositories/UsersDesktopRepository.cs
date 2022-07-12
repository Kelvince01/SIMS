using System;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class UsersDesktopRepository :
        RepositoryBase<UsersDesktop>,
        IUsersDesktopRepository,
        IRepository<UsersDesktop>
    {
        public UsersDesktopRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public UsersDesktop IsValidUsers(string userName, string password) => this.DbContext.UsersDesktops.Where<UsersDesktop>((Expression<Func<UsersDesktop, bool>>)(m => m.UserId == userName && m.Password == password && m.isActive == "Y")).FirstOrDefault<UsersDesktop>();
    }
}
