using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public interface IUsersDesktopRepository : IRepository<UsersDesktop>
    {
        UsersDesktop IsValidUsers(string userName, string password);
    }
}
