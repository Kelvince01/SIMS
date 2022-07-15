using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIMS.BLL;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls.Setups
{
    /// <summary>
    /// Interaction logic for ucStaffSetup.xaml
    /// </summary>
    public partial class ucStaffSetup : BaseUserControl
    {
        private IStaffService _service;

        public ucStaffSetup()
        {
            InitializeComponent();
            this._service = (IStaffService)new StaffService((IDbFactory)new DbFactory());
            this.SetTheme();
        }

        public event ucStaffSetup.afterCloseClick onCloseClick;

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.onCloseClick == null)
                return;
            this.onCloseClick((object)this);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this record ?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                    return;
                if (this.txtCardNo.Tag == null)
                {
                    int num1 = (int)MessageBox.Show("No Record selected for delete");
                }
                else if (this.txtCardNo.Tag != null)
                {
                    this._service.Remove(this.txtCardNo.Tag as Staff);
                    this._service.Save();
                    this.LoadGridData();
                    this.ClearAll();
                    this.GenerateMax();
                    int num2 = (int)MessageBox.Show("PGroup update successfull");
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtName.Text.Trim() == "")
                {
                    int num = (int)MessageBox.Show("Enter customer name");
                    this.txtName.Focus();
                }
                else if (this.txtCardNo.Tag == null)
                {
                    this._service.CreateUpdate(new Staff()
                    {
                        Address = this.txtAddress.Text,
                        CardNo = this.txtCardNo.Text,
                        DesignationId = new int?(int.Parse(this.cmbDesignation.SelectedValue.ToString())),
                        Email = this.txtEmail.Text,
                        StaffId = int.Parse(this.GenerateMax()),
                        StaffName = this.txtName.Text,
                        IsActive = (bool)this.cbIsActive.IsChecked ? "1" : "0",
                        Mobile = this.txtMobile.Text
                    });
                    this._service.Save();
                    this.LoadGridData();
                    this.ClearAll();
                    this.GenerateMax();
                    int num = (int)MessageBox.Show("Customer add successfull");
                }
                else if (this.txtCardNo.Tag != null)
                {
                    Staff tag = this.txtCardNo.Tag as Staff;
                    tag.Address = this.txtAddress.Text;
                    tag.CardNo = this.txtCardNo.Text;
                    tag.DesignationId = new int?(int.Parse(this.cmbDesignation.SelectedValue.ToString()));
                    tag.Email = this.txtEmail.Text;
                    tag.StaffName = this.txtName.Text;
                    tag.IsActive = (bool)this.cbIsActive.IsChecked ? "1" : "0";
                    tag.Mobile = this.txtMobile.Text;
                    this._service.Update(tag);
                    this._service.Save();
                    this.LoadGridData();
                    this.ClearAll();
                    this.GenerateMax();
                    int num = (int)MessageBox.Show("Customer update successfull");
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void ucStaffSetup_Load(object sender, EventArgs e)
        {
            List<Designation> list = new DesignationService((IDbFactory)new DbFactory()).Gets((string)null).ToList<Designation>();
            this.cmbDesignation.DisplayMemberPath = "DesignationName";
            this.cmbDesignation.SelectedValuePath = "DesignationId";
            this.cmbDesignation.ItemsSource = list;
            this.LoadGridData();
        }

        private void LoadGridData() => this.dgvList.ItemsSource = this._service.Gets().ToList<Staff>();

        private string GenerateMax() => new GlobalClass().GetMaxId("StaffId", "Staff");

        private void ClearAll()
        {
            this.txtCardNo.Text = this.txtName.Text = this.txtMobile.Text = this.txtName.Text = this.txtAddress.Text = this.txtEmail.Text = "";
            this.txtCardNo.Tag = (object)null;
            this.btnDelete.IsEnabled = false;
        }

        private void ucStaffSetup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;
            //this.ProcessTabKey(true);
            FrameworkElement element = Keyboard.FocusedElement as FrameworkElement;
            if (element != null)
                element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        public delegate void afterCloseClick(object sender);

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvList.SelectedItem == null)
                return;

            DataGridRow row = sender as DataGridRow;
            var selectedStaff = dgvList.SelectedItem as Staff;
            Staff staff = this._service.Get(selectedStaff.StaffId);
            this.ClearAll();
            if (staff == null)
                return;
            this.txtCardNo.Text = staff.CardNo;
            this.txtCardNo.Tag = (object)staff;
            this.txtName.Text = staff.StaffName;
            this.txtAddress.Text = staff.Address;
            this.txtCardNo.Text = staff.CardNo;
            this.cmbDesignation.SelectedValue = (object)staff.DesignationId;
            this.txtEmail.Text = staff.Email;
            this.cbIsActive.IsChecked = staff.IsActive == "1";
            this.txtMobile.Text = staff.Mobile;
            this.btnDelete.IsEnabled = true;
        }
    }
}
