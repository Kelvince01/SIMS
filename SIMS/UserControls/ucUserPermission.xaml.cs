using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls
{
    /// <summary>
    /// Interaction logic for ucUserPermission.xaml
    /// </summary>
    public partial class ucUserPermission : BaseUserControl
    {
        private IUsersDesktopService _serviceUser;
        private IDesktopMenuService _serviceMenu;
        private IUsersDesktopMenusService _serviceUsersMenus;

        public ucUserPermission()
        {
            InitializeComponent();
            IDbFactory idbFactory = (IDbFactory)new DbFactory();
            this._serviceMenu = (IDesktopMenuService)new DesktopMenuService(idbFactory);
            this._serviceUser = (IUsersDesktopService)new UsersDesktopService(idbFactory);
            this._serviceUsersMenus = (IUsersDesktopMenusService)new UsersDesktopMenusService(idbFactory);
            this.SetTheme();
        }

        private void ucUserPermission_Load(object sender, EventArgs e) => this.LoadUser();

        public event ucUserPermission.afterCloseClick onCloseClick;

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.onCloseClick == null)
                return;
            this.onCloseClick((object)this);
        }

        private void LoadUser()
        {
            List<UsersDesktop> list = this._serviceUser.Gets().ToList<UsersDesktop>();
            list.Insert(0, new UsersDesktop()
            {
                UserId = "Select"
            });
            this.cmbUsers.ItemsSource = list;
            this.cmbUsers.DisplayMemberPath = "UserId";
            this.cmbUsers.SelectedValuePath = "UserId";
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbUsers.SelectedIndex <= 0)
                return;
            this.LoadAllMenuInGrid();
            this.LoadPermitedItem();
        }

        private void LoadAllMenuInGrid()
        {
            List<DesktopMenu> desktopMenuList = this._serviceMenu.SelectAllParentMenu();
            //this.dgvList.Columns[1].DataPropertyName = "MenuTitle";
            //this.dgvList.Columns[2].DataPropertyName = "UMenuID";
            this.dgvList.ItemsSource = desktopMenuList;
            this.dgvList.AutoGenerateColumns = false;
        }

        private void LoadPermitedItem()
        {
            UsersDesktopMenu usersDesktopMenu = this._serviceUsersMenus.Gets(this.cmbUsers.Text).FirstOrDefault<UsersDesktopMenu>();
            if (usersDesktopMenu == null)
                return;
            string[] strArray = usersDesktopMenu.UMenuID.Split(',');
            /*foreach (DataGridRow row in (IEnumerable)this.dgvList.Rows)
            {
                DataGridCell cell = row.Cells[2];
                foreach (string str in strArray)
                {
                    if (str == cell.Value.ToString())
                    {
                        row.Cells[0].Value = (object)true;
                        break;
                    }
                }
            }*/
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbUsers.SelectedIndex != 0)
                {
                    /*this.dgvList.EndEdit();
                    if (this.dgvList.RowCount <= 1)
                        return;
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (DataGridRow row in (IEnumerable)this.dgvList.Rows)
                    {
                        string str = row.Cells[2].Value.ToString();
                        if (row.Cells[0].Value != null && (bool)row.Cells[0].Value)
                            stringBuilder.Append(str).Append(",");
                    }
                    this._serviceUsersMenus.Create(new UsersDesktopMenu()
                    {
                        UMenuID = Convert.ToString((object)stringBuilder.Remove(stringBuilder.Length - 1, 1)),
                        UserName = this.cmbUsers.Text
                    });*/
                    this._serviceUsersMenus.Save();
                    this.ClearAll();
                    int num = (int)MessageBox.Show("Data Saved Successfully.");
                }
                else
                {
                    int num1 = (int)MessageBox.Show("Please select user!");
                }
            }
            catch (DbEntityValidationException ex)
            {
                Exception innerException = (Exception)ex;
                foreach (DbEntityValidationResult entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError in (IEnumerable<DbValidationError>)entityValidationError.ValidationErrors)
                        innerException = (Exception)new InvalidOperationException(string.Format("{0}:{1}", (object)entityValidationError.Entry.Entity.ToString(), (object)validationError.ErrorMessage), innerException);
                }
                throw innerException;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void ClearAll()
        {
        }

        public delegate void afterCloseClick(object sender);
    }
}
