using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using SIMS.BLL;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls.Setups
{
    /// <summary>
    /// Interaction logic for ucGroupSetup.xaml
    /// </summary>
    public partial class ucGroupSetup : BaseUserControl
    {
        private IPGroupService _service;
        private DispatcherTimer timer1;

        public ucGroupSetup()
        {
            InitializeComponent();
            this._service = (IPGroupService)new PGroupService((IDbFactory)new DbFactory());
            this.SetTheme();
        }

        public event ucGroupSetup.afterCloseClick onCloseClick;

        //private void LoadGridData() => this.dgvList.DataSource = (object)this._service.Gets().ToList<PGroup>();

        private void GenerateMax()
        {
            //this.txtGroupId.Text = new GlobalClass().GetMaxId("GroupID", "00", "01", "PGroup");
            // this.txtGroupName.Focus();
        }

        private void ClearAll()
        {
            /*this.txtGroupId.Text = this.txtGroupName.Text = this.txtFloorId.Text = this.txtFloorId.Text = this.txtDisc.Text = this.txtCostOnSale.Text = this.txtVat.Text = "";
            this.txtGroupId.Tag = (object)null;
            this.btnDelete.Enabled = false;
            this.txtGroupName.Focus();*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.IsEnabled = false;
            this.GenerateMax();
            //this.LoadGridData();
            //this.txtGroupName.Focus();
        }

        public delegate void afterCloseClick(object sender);
    }
}
