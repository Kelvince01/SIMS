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
using System.Windows.Threading;
using SIMS.BLL;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls
{
    /// <summary>
    /// Interaction logic for ucProfileUpdate.xaml
    /// </summary>
    public partial class ucProfileUpdate : UserControl
    {
        private IUsersDesktopService _serviceUser;
        private UsersDesktop ud;

        private DispatcherTimer timer1;

        public ucProfileUpdate()
        {
            InitializeComponent();
            this._serviceUser = (IUsersDesktopService)new UsersDesktopService((IDbFactory)new DbFactory());

            this.timer1.IsEnabled = true;
            this.timer1.Interval = new TimeSpan(1000);
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
        }

        public event ucProfileUpdate.afterCloseClick onCloseClick;

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.onCloseClick == null)
                return;
            this.onCloseClick((object)this);
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtUserId.Text == "")
                {
                    int num1 = (int)MessageBox.Show("Enter Login Id");
                }
                else
                {
                    if (this.btnSave.Content == "Update")
                    {
                        this.ud.FullName = this.txtFullName.Text;
                        if (this.txtNewPassword.Text != "")
                        {
                            if (this.ud.Password != GlobalClass.GetEncryptedPassword(this.txtCurrentPassword.Text))
                            {
                                int num2 = (int)MessageBox.Show("Current password is not valid");
                                return;
                            }
                            if (this.txtNewPassword.Text != this.txtConfirmPassword.Text)
                            {
                                int num3 = (int)MessageBox.Show("Password and confirm password is not valid");
                                return;
                            }
                            this.ud.Password = GlobalClass.GetEncryptedPassword(this.txtNewPassword.Text);
                        }
                        this.ud.Address = this.txtAddress.Text;
                        this._serviceUser.Update(this.ud);
                    }
                    this._serviceUser.Save();
                    this.txtConfirmPassword.Text = this.txtCurrentPassword.Text = this.txtNewPassword.Text = "";
                    int num4 = (int)MessageBox.Show("User Information Save Successfully");
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.IsEnabled = false;
            this.ud = this._serviceUser.Get(StaticData.UserId);
            if (this.ud == null)
                return;
            this.txtUserId.Text = this.ud.UserId;
            this.txtMobile.Text = this.ud.Email;
            this.txtFullName.Text = this.ud.FullName;
            this.txtAddress.Text = this.ud.Address;
            this.txtUserId.IsEnabled = false;
            this.btnSave.Content = "Update";
        }

        public delegate void afterCloseClick(object sender);
    }
}
