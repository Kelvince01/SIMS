using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public interface ICampusRepositroy : IRepository<Campus>
    {
        Campus GetById(string campusId);

        void RemoveAll();
    }
}
