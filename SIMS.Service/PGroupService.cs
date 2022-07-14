using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class PGroupService : IPGroupService
    {
        public readonly IPGroupRepostiry _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PGroupService(IDbFactory idbFactory)
        {
            this._repository = (IPGroupRepostiry)new PGroupRepostiry(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<PGroup> Gets(string name = null) => string.IsNullOrEmpty(name) ? (IEnumerable<PGroup>)this._repository.GetAll().OrderBy<PGroup, string>((Func<PGroup, string>)(m => m.GroupName)) : (IEnumerable<PGroup>)this._repository.GetAll().OrderBy<PGroup, string>((Func<PGroup, string>)(m => m.GroupName));

        public PGroup Get(Decimal id) => this._repository.GetById(id);

        public PGroup Get(string name) => (PGroup)null;

        public void Create(PGroup model)
        {
            if (this._repository.GetMany((Expression<Func<PGroup, bool>>)(m => m.GroupName == model.GroupName)).FirstOrDefault<PGroup>() != null)
                throw new Exception("This record already enterd");
            this._repository.Add(model);
        }

        public void Update(PGroup model)
        {
            if (this._repository.GetMany((Expression<Func<PGroup, bool>>)(m => m.GroupName == model.GroupName && m.GroupID != model.GroupID)).FirstOrDefault<PGroup>() != null)
                throw new Exception("This record already enterd");
            this._repository.Update(model);
        }

        public void Remove(PGroup model) => this._repository.Delete(model);

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(PGroup model) => throw new NotImplementedException();

        public PGroup GetById(string PGroupId) => this._repository.GetMany((Expression<Func<PGroup, bool>>)(m => m.GroupID == PGroupId)).FirstOrDefault<PGroup>();
    }
}
