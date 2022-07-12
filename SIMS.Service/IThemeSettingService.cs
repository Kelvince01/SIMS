using SIMS.Models;

namespace SIMS.Service
{
    public interface IThemeSettingService
    {
        void Update(ThemeSetting model);

        void Save();

        ThemeSetting GetTopSetup();
    }
}
