using System;
using System.Collections.Generic;
using System.Linq;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class UsersDesktopMenusService : IUsersDesktopMenusService
    {
        private readonly IUsersDesktopMenusRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UsersDesktopMenusService(IDbFactory idbFactory)
        {
            this._repository = (IUsersDesktopMenusRepository)new UsersDesktopMenusRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<UsersDesktopMenu> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._repository.GetAll() : this._repository.GetAll().Where<UsersDesktopMenu>((Func<UsersDesktopMenu, bool>)(c => c.UserName == name));

        public UsersDesktopMenu Get(Decimal id) => this._repository.GetById(id);

        public void Create(UsersDesktopMenu model) => this._repository.Add(model);

        public void Update(UsersDesktopMenu model) => this._repository.Update(model);

        public void Remove(UsersDesktopMenu model) => this._repository.Delete(model);

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(UsersDesktopMenu model) => throw new NotImplementedException();
    }
}
