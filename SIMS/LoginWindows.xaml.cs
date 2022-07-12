using SIMS.Data.Infrastructure;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS.BLL;
using SIMS.Models;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for LoginWindows.xaml
    /// </summary>
    public partial class LoginWindows : Window
    {
        private IUsersDesktopService _service;
        public bool isLogIn = false;

        public LoginWindows()
        {
            InitializeComponent();
            this._service = (IUsersDesktopService)new UsersDesktopService((IDbFactory)new DbFactory());
        }

        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.txtUserId.Text == "")
            {
                int num1 = (int)MessageBox.Show("Enter User Id");
            }
            else if (this.txtPassowrd.Password == "")
            {
                int num2 = (int)MessageBox.Show("Enter Password");
            }
            else if (this.txtUserId.Text == StaticData.SystemUser && this.txtPassowrd.Password == StaticData.SystemUserPass)
            {
                StaticData.UserId = StaticData.SystemUser;
                StaticData.UserName = StaticData.SystemUser;
                this.isLogIn = true;
                this.Close();
            }
            else
            {
                UsersDesktop usersDesktop = this._service.IsValidUsers(this.txtUserId.Text, GlobalClass.GetEncryptedPassword(this.txtPassowrd.Password));
                if (usersDesktop != null)
                {
                    StaticData.UserId = usersDesktop.UserId;
                    StaticData.UserName = usersDesktop.FullName;
                    this.isLogIn = true;
                    this.Close();
                }
                else
                {
                    this.txtUserId.Focus();
                    MessageBox.Show(this, "\r\n Invalid user or password combination", "SIMS", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.isLogIn = false;
            this.Close();
        }

        private void LoginWindows_OnLoaded(object sender, RoutedEventArgs e)
        {
            //this.lblCounter.Text = StaticData.CounterName;
            //this.lblShop.Text = StaticData.ShopName;
            if (Debugger.IsAttached)
            {
                this.txtUserId.Text = StaticData.SystemUser;
                this.txtPassowrd.Password = StaticData.SystemUserPass;
            }
            //this.panel1.Location = new Point((this.Width - this.panel1.Width - 10) / 2, (this.Height - this.panel1.Height) / 2);
            //this.pictureBox2.Location = new Point(this.Width - this.pictureBox2.Width - 10, this.Height - this.pictureBox2.Height - 10);
        }

        private void LoginWindows_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyStates != KeyStates.Down)
                return;
            if (this.txtUserId.Focus())
                this.txtPassowrd.Focus();
            else if (this.txtPassowrd.Focus())
                this.btnLogin.Focus();
        }
    }
}
