using System;
using System.Collections.Generic;
using System.Linq;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class DesktopMenuService : IDesktopMenuService
    {
        private readonly IDesktopMenuRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DesktopMenuService(IDbFactory idbFactory)
        {
            this._repository = (IDesktopMenuRepository)new DesktopMenuRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<DesktopMenu> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._repository.GetAll() : this._repository.GetAll().Where<DesktopMenu>((Func<DesktopMenu, bool>)(c => c.MenuTitle == name));

        public DesktopMenu Get(Decimal id) => this._repository.GetById(id);

        public void Create(DesktopMenu model) => this._repository.Add(model);

        public void Update(DesktopMenu model) => this._repository.Update(model);

        public void Remove(DesktopMenu model) => this._repository.Delete(model);

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(DesktopMenu model) => throw new NotImplementedException();

        public List<DesktopMenu> SelectAllParentMenu()
        {
            IEnumerable<DesktopMenu> all = this._repository.GetAll();
            foreach (DesktopMenu desktopMenu in all)
                desktopMenu.Checked = false;
            return all.ToList<DesktopMenu>();
        }
    }
}
