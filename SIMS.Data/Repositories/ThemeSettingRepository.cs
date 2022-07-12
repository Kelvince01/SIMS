using System;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class ThemeSettingRepository :
        RepositoryBase<ThemeSetting>,
        IThemeSettingRepository,
        IRepository<ThemeSetting>
    {
        public ThemeSettingRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public ThemeSetting GetTopSetup() => this.DbContext.ThemeSettings.Select<ThemeSetting, ThemeSetting>((Expression<Func<ThemeSetting, ThemeSetting>>)(m => m)).FirstOrDefault<ThemeSetting>();
    }
}
