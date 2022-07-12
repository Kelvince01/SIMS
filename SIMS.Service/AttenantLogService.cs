using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class AttenantLogService : IAttenantLogService
    {
        private readonly IAttenantLogRepository _attentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AttenantLogService(IDbFactory idbFactory)
        {
            this._attentRepository = (IAttenantLogRepository)new AttenantLogRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<AttenantLog> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._attentRepository.GetAll() : this._attentRepository.GetAll().Where<AttenantLog>((Func<AttenantLog, bool>)(c => c.ShopID == name));

        public AttenantLog Get(Decimal id) => this._attentRepository.GetById(id);

        public AttenantLog GetByShopAndDate(string name, DateTime date) => this._attentRepository.GetMany((Expression<Func<AttenantLog, bool>>)(m => m.ShopID == name && DbFunctions.TruncateTime((DateTime?)m.InTime.Value) == DbFunctions.TruncateTime((DateTime?)date))).FirstOrDefault<AttenantLog>();

        public void Create(AttenantLog model) => this._attentRepository.Add(model);

        public void Update(AttenantLog model) => this._attentRepository.Update(model);

        public void Remove(AttenantLog model) => this._attentRepository.Delete(model);

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(AttenantLog model) => throw new NotImplementedException();
    }
}
