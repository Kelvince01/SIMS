using System.Windows.Controls;
using SIMS.Data.Infrastructure;
using SIMS.Service;

namespace SIMS.UserControls
{
    public class BaseUserControl : UserControl
    {
        private IThemeSettingService _serviceThemedata;
        //public MetroStyleManager metroStyleManager;
        //public MetroStyleExtender metroStyleExtender;

        public BaseUserControl()
        {
        }

        public void SetTheme()
        {
            this._serviceThemedata = (IThemeSettingService)new ThemeSettingService((IDbFactory)new DbFactory());
            //this.metroStyleManager.Style = (MetroColorStyle)Enum.Parse(typeof(MetroColorStyle), this._serviceThemedata.GetTopSetup().ButtonColor);
        }
    }
}
