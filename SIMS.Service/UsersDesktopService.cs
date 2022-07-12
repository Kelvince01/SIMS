using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class UsersDesktopService : IUsersDesktopService
    {
        public readonly IUsersDesktopRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UsersDesktopService(IDbFactory idbFactory)
        {
            this._repository = (IUsersDesktopRepository)new UsersDesktopRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<UsersDesktop> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._repository.GetAll() : this._repository.GetAll();

        public UsersDesktop Get(Decimal id) => this._repository.GetById(id);

        public UsersDesktop Get(string name) => this._repository.GetMany((Expression<Func<UsersDesktop, bool>>)(m => m.UserId == name)).FirstOrDefault<UsersDesktop>();

        public void Create(UsersDesktop model) => this._repository.Add(model);

        public void Update(UsersDesktop model) => this._repository.Update(model);

        public void Remove(UsersDesktop model) => this._repository.Delete(model);

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(UsersDesktop model, bool isNew) => this._repository.GetMany((Expression<Func<UsersDesktop, bool>>)(m => m.UserId == model.UserId)).FirstOrDefault<UsersDesktop>() != null && isNew;

        public UsersDesktop IsValidUsers(string userName, string password) => this._repository.IsValidUsers(userName, password);
    }
}
