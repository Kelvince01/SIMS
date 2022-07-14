using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class PersonRepository :
        RepositoryBase<Person>,
        IPersonRepository,
        IRepository<Person>
    {
        public PersonRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
