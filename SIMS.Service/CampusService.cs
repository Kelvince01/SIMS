using System;
using System.Collections.Generic;
using System.Linq;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class CampusService : ICampusService
    {
        public readonly ICampusRepositroy _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CampusService(IDbFactory idbFactory)
        {
            this._repository = (ICampusRepositroy)new CampusRepositroy(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<Campus> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._repository.GetAll() : this._repository.GetAll();

        public Campus Get(Decimal id) => this._repository.GetById(id);

        public Campus Get(string name) => (Campus)null;

        public void CreateUpdate(Campus model) => this._repository.Add(model);

        public void Update(Campus model) => this._repository.Update(model);

        public void Remove(Campus model) => this._repository.Delete(model);

        public void RemoveAll() => this._repository.RemoveAll();

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(Campus model) => throw new NotImplementedException();

        public Campus GetById(string shopId) => this._repository.GetById(shopId);

        public List<Campus> GetServerCampus()
        {
            Result result = new SQLDAL("Server").Select("SELECT CampusID ,CampusName ,VillAreaRoad ,Post ,Pstation ,\r\n                                           District ,Contact ,Phone ,VATDisabled ,\r\n                                           VatRegNo FROM dbo.Campus");
            if (!result.ExecutionState)
                return (List<Campus>)null;
            List<Campus> serverCampus = new List<Campus>();
            for (int index = 0; index < result.Data.Rows.Count; ++index)
                serverCampus.Add(new Campus()
                {
                    CampusID = result.Data.Rows[index]["CampusID"].ToString(),
                    CampusName = result.Data.Rows[index]["CampusName"].ToString(),
                    VillAreaRoad = result.Data.Rows[index]["VillAreaRoad"].ToString(),
                    Post = result.Data.Rows[index]["Post"].ToString(),
                    Pstation = result.Data.Rows[index]["Pstation"].ToString(),
                    District = result.Data.Rows[index]["District"].ToString(),
                    Contact = result.Data.Rows[index]["Contact"].ToString(),
                    Phone = result.Data.Rows[index]["Phone"].ToString(),
                });
            return serverCampus;
        }

        public List<Campus> GetCampus(string defaultText)
        {
            List<Campus> list = this._repository.GetAll().ToList<Campus>();
            list.Insert(0, new Campus()
            {
                CampusID = defaultText,
                CampusName = defaultText
            });
            return list;
        }
    }
}
