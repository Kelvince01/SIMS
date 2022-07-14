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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.BLL;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls.Setups
{
    /// <summary>
    /// Interaction logic for ucPersonSetup.xaml
    /// </summary>
    public partial class ucPersonSetup : BaseUserControl
    {
        private IPersonService _service;

        public ucPersonSetup()
        {
            InitializeComponent();
            this._service = (IPersonService)new PersonService((IDbFactory)new DbFactory());
            this.SetTheme();
        }

        public event ucPersonSetup.afterCloseClick onCloseClick;

        public delegate void afterCloseClick(object sender);

        private void UcPersonSetup_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;
            //this.ProcessTabKey(true);
        }

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
                if (this.txtFirstName.Text.Trim() == "" && this.txtLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Enter customer name");
                    this.txtFirstName.Focus();
                }
                /*else if (this.txtCardNo.Tag == null)
                {
                    this._service.CreateUpdate(new Person()
                    {
                        FirstName = this.txtFirstName.Text,
                        LastName = this.txtLastName.Text,
                        Discriminator = this.cmbDiscriminator.SelectedValue.ToString(),
                        HireDate = this.dpHireDate.SelectedDateTime,
                        PersonID = int.Parse(this.GenerateMax()),
                    });
                    //IsActive = this.cbIsActive.Checked ? "1" : "0",
                    this._service.Save();
                    this.LoadGridData();
                    this.ClearAll();
                    this.GenerateMax();
                    int num = (int)MessageBox.Show("Customer add successfull");
                }
                else if (this.txtCardNo.Tag != null)
                {
                    Person tag = this.txtCardNo.Tag as Person;
                    tag.FirstName = this.txtFirstName.Text;
                    tag.CardNo = this.txtCardNo.Text;
                    tag.Discriminator = this.cmbDiscriminator.SelectedValue.ToString();
                    tag.LastName = this.txtLastName.Text;
                    tag.HireDate = this.dpHireDate.SelectedDateTime;
                    this._service.Update(tag);
                    this._service.Save();
                    this.LoadGridData();
                    this.ClearAll();
                    this.GenerateMax();
                    int num = (int)MessageBox.Show("Customer update successfull");
                }*/
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this record ?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                    return;
                /*if (this.txtCardNo.Tag == null)
                {
                    MessageBox.Show("No Record selected for delete");
                }
                else if (this.txtCardNo.Tag != null)
                {
                    this._service.Remove(this.txtCardNo.Tag as Person);
                    this._service.Save();
                    this.LoadGridData();
                    this.ClearAll();
                    this.GenerateMax();
                    int num2 = (int)MessageBox.Show("Person update successfull");
                }*/
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void LoadGridData() => this.dgvList.ItemsSource = this._service.Gets().ToList<Person>();

        private string GenerateMax() => new GlobalClass().GetMaxId("PersonID", "Person");

        private void ClearAll()
        {
            /*this.txtCardNo.Text = this.txtFirstName.Text = this.txtLastName.Text = (string)(this.cmbDiscriminator.SelectedValue = "");
            this.txtCardNo.Tag = (object)null;*/
            this.btnDelete.IsEnabled = false;
        }

        private void UcPersonSetup_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<Discriminator> list = new DiscriminatorService((IDbFactory)new DbFactory()).Gets((string)null).ToList<Discriminator>();
            this.cmbDiscriminator.DisplayMemberPath = "DiscriminatorName";
            this.cmbDiscriminator.SelectedValuePath = "DiscriminatorId";
            this.cmbDiscriminator.ItemsSource = list;
            this.LoadGridData();
        }

        private void CPersonId_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            /*if (e.RowIndex == -1)
                return;
            Employee employee = this._service.Get(Decimal.Parse(this.dgvList.Rows[e.RowIndex].Cells["cEmployeeId"].Value.ToString()));
            this.ClearAll();
            if (employee == null)
                return;
            this.txtCardNo.Text = employee.CardNo;
            this.txtCardNo.Tag = (object)employee;
            this.txtName.Text = employee.EmpName;
            this.txtAddress.Text = employee.Address;
            this.txtCardNo.Text = employee.CardNo;
            this.cmbDesignation.SelectedValue = (object)employee.DesignationId;
            this.txtEmail.Text = employee.Email;
            this.cbIsActive.Checked = employee.IsActive == "1";
            this.txtMobile.Text = employee.Mobile;
            this.btnDelete.Enabled = true;*/
        }
    }
}
