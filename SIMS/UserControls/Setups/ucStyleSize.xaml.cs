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
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls.Setups
{
    /// <summary>
    /// Interaction logic for ucStyleSize.xaml
    /// </summary>
    public partial class ucStyleSize : BaseUserControl
    {
        private IStyleSizeService _service;
        private DispatcherTimer timer1;

        public ucStyleSize()
        {
            InitializeComponent();
            this._service = (IStyleSizeService)new StyleSizeService((IDbFactory)new DbFactory());
            this.SetTheme();
        }

        public event ucStyleSize.afterCloseClick onCloseClick;

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*this.timer1.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            this.lblStatus.Text = "Loading Data .....";
            Application.DoEvents();
            this.dgvList.DataSource = (object)this._service.GetStyleSizeBySearch(this.txtSearch.Text, true, "All").ToList<StyleSize>();
            bool? supplierWiseStock = StaticData.globalSetup.IsSupplierWiseStock;
            if ((supplierWiseStock.GetValueOrDefault() ? 0 : (supplierWiseStock.HasValue ? 1 : 0)) != 0)
                this.dgvList.Columns["cSupplier"].Visible = false;
            Cursor.Current = Cursors.Default;
            this.lblStatus.Text = "";
            Application.DoEvents();*/
        }

        public delegate void afterCloseClick(object sender);
    }
}
