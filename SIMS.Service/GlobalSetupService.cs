using System;
using System.Linq;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class GlobalSetupService : IGlobalSetupService
    {
        public readonly IGlobalSetupRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GlobalSetupService(IDbFactory idbFactory)
        {
            this._repository = (IGlobalSetupRepository)new GlobalSetupRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public GlobalSetup GetTopSetup() => this._repository.GetTopSetup();

        public void Update(GlobalSetup model) => this._repository.Update(model);

        public void Save() => this._unitOfWork.Commit();

        public void UpdateFiscalYear(string[] model)
        {
            GlobalSetup globalSetup = this._repository.GetAll().FirstOrDefault<GlobalSetup>();
            if (!string.IsNullOrEmpty(model[0]))
                globalSetup.FiscYr = new DateTime?(DateTime.Parse(model[0]));
            if (string.IsNullOrEmpty(model[0]))
                return;
            globalSetup.FiscYr2 = new DateTime?(DateTime.Parse(model[1]));
        }
    }
}
