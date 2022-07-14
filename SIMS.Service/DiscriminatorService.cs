using System;
using System.Collections.Generic;
using System.Linq;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class DiscriminatorService : IDiscriminatorService
    {
        public readonly IDiscriminatorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DiscriminatorService(IDbFactory idbFactory)
        {
            this._repository = (IDiscriminatorRepository)new DiscriminatorRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<Discriminator> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._repository.GetAll() : this._repository.GetAll().Where<Discriminator>((Func<Discriminator, bool>)(c => c.DiscriminatorName == name));

        public Discriminator Get(Decimal id) => this._repository.GetById(id);

        public Discriminator Get(string name) => (Discriminator)null;

        public void CreateUpdate(Discriminator model) => this._repository.Add(model);

        public void Update(Discriminator model) => this._repository.Update(model);

        public void Remove(Discriminator model) => this._repository.Delete(model);

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(Discriminator model) => false;

        public List<Discriminator> GetServerDiscriminator()
        {
            Result result = new SQLDAL("Server").Select("SELECT [DiscriminatorId]\r\n                                                            ,[DiscriminatorName]\r\n                                                            ,[SortOrder]\r\n                                                        FROM [Discriminator]");
            if (!result.ExecutionState)
                return (List<Discriminator>)null;
            List<Discriminator> serverDiscriminator = new List<Discriminator>();
            for (int index = 0; index < result.Data.Rows.Count; ++index)
                serverDiscriminator.Add(new Discriminator()
                {
                    DiscriminatorId = int.Parse(result.Data.Rows[index]["DiscriminatorId"].ToString()),
                    DiscriminatorName = result.Data.Rows[index]["DiscriminatorName"].ToString(),
                    SortOrder = new int?(int.Parse(result.Data.Rows[index]["SortOrder"].ToString()))
                });
            return serverDiscriminator;
        }
    }
}
