using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class ThemeSettingService : IThemeSettingService
    {
        public readonly IThemeSettingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ThemeSettingService(IDbFactory idbFactory)
        {
            this._repository = (IThemeSettingRepository)new ThemeSettingRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public ThemeSetting GetTopSetup() => this._repository.GetTopSetup();

        public void Update(ThemeSetting model) => this._repository.Update(model);

        public void Save() => this._unitOfWork.Commit();
    }
}
