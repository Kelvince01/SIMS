using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public interface IThemeSettingRepository : IRepository<ThemeSetting>
    {
        ThemeSetting GetTopSetup();
    }
}
