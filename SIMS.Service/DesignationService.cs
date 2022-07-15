using System;
using System.Collections.Generic;
using System.Linq;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class DesignationService : IDesignationService
    {
        public readonly IDesignationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DesignationService(IDbFactory idbFactory)
        {
            this._repository = (IDesignationRepository)new DesignationRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<Designation> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._repository.GetAll() : this._repository.GetAll().Where<Designation>((Func<Designation, bool>)(c => c.DesignationName == name));

        public Designation Get(Decimal id) => this._repository.GetById(id);

        public Designation Get(string name) => (Designation)null;

        public void CreateUpdate(Designation model) => this._repository.Add(model);

        public void Update(Designation model) => this._repository.Update(model);

        public void Remove(Designation model) => this._repository.Delete(model);

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(Designation model) => false;

        public List<Designation> GetServerDesignation()
        {
            Result result = new SQLDAL("Server").Select("SELECT [DesignationId]\r\n                                                            ,[DesignationName]\r\n                                                            ,[SortOrder]\r\n                                                        FROM [Designation]");
            if (!result.ExecutionState)
                return (List<Designation>)null;
            List<Designation> serverDesignation = new List<Designation>();
            for (int index = 0; index < result.Data.Rows.Count; ++index)
                serverDesignation.Add(new Designation()
                {
                    DesignationId = int.Parse(result.Data.Rows[index]["DesignationId"].ToString()),
                    DesignationName = result.Data.Rows[index]["DesignationName"].ToString(),
                    SortOrder = new int?(int.Parse(result.Data.Rows[index]["SortOrder"].ToString()))
                });
            return serverDesignation;
        }
    }
}
