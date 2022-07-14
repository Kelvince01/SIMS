using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CustomControls;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls
{
    /// <summary>
    /// Interaction logic for ucThemeSettings.xaml
    /// </summary>
    public partial class ucThemeSettings : BaseUserControl
    {
        private IThemeSettingService _service;
        private ColorDialog colorDialog1;

        public ucThemeSettings()
        {
            InitializeComponent();
            this._service = (IThemeSettingService)new ThemeSettingService((IDbFactory)new DbFactory());
            this.SetTheme();
        }

        public event ucThemeSettings.afterCloseClick onCloseClick;

        private void BtnSelectColor_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.colorDialog1.ShowDialog() != true)
                return;
            this.btnSelectColor.Background = new SolidColorBrush(ColorToColor(this.colorDialog1.Color));
            this.btnSave.Focus();
            this.lblBackColor.Content = "Current Color : " + this.colorDialog1.Color.ToArgb().ToString();
        }

        public System.Windows.Media.Color ColorToColor(System.Drawing.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private void BtnBlack_OnClick(object sender, RoutedEventArgs e)
        {
            this.lblButtonColor.Content = "Current Color : " + (sender as Button).Content;
        }

        private void UcThemeSettings_OnLoaded(object sender, RoutedEventArgs e)
        {
            ThemeSetting topSetup = this._service.GetTopSetup();
            if (topSetup == null)
                return;
            this.lblBackColor.Content = "Current Color : " + topSetup.BackgroundCOlor;
            this.lblButtonColor.Content = "Current Color : " + topSetup.ButtonColor;
            this.btnSelectColor.Background = new SolidColorBrush(ColorToColor(System.Drawing.Color.FromArgb(int.Parse(topSetup.BackgroundCOlor))));
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ThemeSetting model = this._service.GetTopSetup() ?? new ThemeSetting();
                model.ButtonColor = this.lblButtonColor.Content.ToString().Split(':')[1].Trim();
                model.BackgroundCOlor = this.lblBackColor.Content.ToString().Split(':')[1].Trim();
                this._service.Update(model);
                this._service.Save();
                int num = (int)MessageBox.Show("Save Successfully ");
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        public delegate void afterCloseClick(object sender);
    }
}
