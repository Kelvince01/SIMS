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
using SIMS.BLL;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls
{
    /// <summary>
    /// Interaction logic for ucUserManagement.xaml
    /// </summary>
    public partial class ucUserManagement : BaseUserControl
    {
        private IUsersDesktopService _serviceUser;
        private UsersDesktop ud;

        public ucUserManagement()
        {
            InitializeComponent();
            this._serviceUser = (IUsersDesktopService)new UsersDesktopService((IDbFactory)new DbFactory());
            this.SetTheme();
        }

        public event ucUserManagement.afterCloseClick onCloseClick;

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.onCloseClick == null)
                return;
            this.onCloseClick((object)this);
        }

        private void ucUserManagement_Load(object sender, EventArgs e)
        {
            this.GridDataBind();
        }

        private void GridDataBind()
        {
            this.dgvList.ItemsSource = this._serviceUser.Gets().ToList<UsersDesktop>();
            this.btnSave.Content = "Save";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtUserId.Text == "")
                {
                    int num1 = (int)MessageBox.Show("Enter Login Id");
                }
                else
                {
                    if (this.btnSave.Content == "Save")
                    {
                        if (this.txtPassword.Text == "")
                        {
                            int num2 = (int)MessageBox.Show("Enter Password Id");
                            return;
                        }
                        this.ud = new UsersDesktop();
                        this.ud.UserId = this.txtUserId.Text;
                        this.ud.Email = this.txtMobile.Text;
                        this.ud.Address = this.txtAddress.Text;
                        this.ud.FullName = this.txtFullName.Text;
                        this.ud.Password = GlobalClass.GetEncryptedPassword(this.txtPassword.Text);
                        this.ud.isActive = (bool)this.cbActive.IsChecked ? "Y" : "N";
                        this.ud.HasHomeButtonEditPermi = (bool)this.cbHomeButtonEdit.IsChecked ? "Y" : "N";
                        if (this._serviceUser.IsDuplicate(this.ud, true))
                        {
                            int num3 = (int)MessageBox.Show("Login Id already taken");
                            return;
                        }
                        this._serviceUser.Create(this.ud);
                    }
                    else if (this.btnSave.Content == "Update")
                    {
                        this.ud.Email = this.txtMobile.Text;
                        this.ud.FullName = this.txtFullName.Text;
                        if (this.txtPassword.Text != "")
                            this.ud.Password = GlobalClass.GetEncryptedPassword(this.txtPassword.Text);
                        this.ud.Address = this.txtAddress.Text;
                        this.ud.isActive = (bool)this.cbActive.IsChecked ? "Y" : "N";
                        this.ud.HasHomeButtonEditPermi = (bool)this.cbHomeButtonEdit.IsChecked ? "Y" : "N";
                        if (this._serviceUser.IsDuplicate(this.ud, false))
                        {
                            int num4 = (int)MessageBox.Show("Login Id already taken");
                            return;
                        }
                        this._serviceUser.Update(this.ud);
                    }
                    this._serviceUser.Save();
                    this.ClearAll();
                    int num5 = (int)MessageBox.Show("User Information Save Successfully");
                    this.GridDataBind();
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        /*private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            string name = this.dgvList.Rows[e.RowIndex].Cells["cUserId"].Value.ToString();
            this.ClearAll();
            this.ud = this._serviceUser.Get(name);
            if (this.ud == null)
                return;
            this.txtUserId.Text = this.ud.UserId;
            this.txtMobile.Text = this.ud.Email;
            this.txtFullName.Text = this.ud.FullName;
            this.txtAddress.Text = this.ud.Address;
            this.cbActive.IsChecked = this.ud.isActive == "Y";
            this.txtUserId.IsEnabled = false;
            this.btnSave.Content = "Update";
        }*/

        private void ClearAll()
        {
            this.txtUserId.Text = "";
            this.txtMobile.Text = "";
            this.txtFullName.Text = "";
            this.txtPassword.Text = "";
            this.txtAddress.Text = "";
            this.cbActive.IsChecked = false;
            this.txtUserId.IsEnabled = true;
            this.btnSave.Content = "Save";
        }

        public delegate void afterCloseClick(object sender);
    }
}
