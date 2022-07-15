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
using System.Windows.Threading;
using SIMS.Data.Infrastructure;
using SIMS.Models;
using SIMS.Service;

namespace SIMS.UserControls
{
    /// <summary>
    /// Interaction logic for ucCampusOnOff.xaml
    /// </summary>
    public partial class ucCampusOnOff : UserControl
    {
        private IAttenantLogService _service;
        private DispatcherTimer timerAttendet;

        public ucCampusOnOff()
        {
            InitializeComponent();
            this._service = (IAttenantLogService)new AttenantLogService((IDbFactory)new DbFactory());

            this.timerAttendet.IsEnabled = true;
            this.timerAttendet.Interval = new TimeSpan(500);
            this.timerAttendet.Tick += new EventHandler(this.timerAttendet_Tick);
        }

        private void ucCampusOnOff_Load(object sender, EventArgs e) => this.timerAttendet.Start();

        public event ucCampusOnOff.afterCloseClick onCloseClick;

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.onCloseClick == null)
                return;
            this.onCloseClick((object)this);
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want Sign In", "Confrimation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                return;
            try
            {
                AttenantLog model = new AttenantLog();
                model.ShopID = StaticData.ShopId;
                model.InTime = new DateTime?(DateTime.Now);
                this._service.Create(model);
                this._service.Save();
                this.btnOn.IsEnabled = false;
                this.btnoff.IsEnabled = false;
                this.lblIn.Content = model.InTime.Value.ToShortTimeString();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void btnoff_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want Sign Out", "Confrimation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                return;
            try
            {
                AttenantLog model = new AttenantLog();
                model.ShopID = StaticData.ShopId;
                model.InTime = new DateTime?(DateTime.Now);
                model.OutTime = new DateTime?(DateTime.Now);
                model.IsTransfer = "N";
                this._service.Create(model);
                this._service.Save();
                this.btnoff.IsEnabled = false;
                this.lblOut.Content = model.OutTime.Value.ToShortTimeString();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void timerAttendet_Tick(object sender, EventArgs e)
        {
            this.timerAttendet.Stop();
            AttenantLog log = this._service.GetByShopAndDate(StaticData.ShopId, DateTime.Now);
            if (log != null)
            {
                this.btnOn.IsEnabled = (log.InTime == null);
                this.btnoff.IsEnabled = (log.OutTime == null);
                this.lblIn.Content = log.InTime.Value.ToShortTimeString();
                if (log.OutTime != null)
                {
                    this.lblOut.Content = log.OutTime.Value.ToShortTimeString();
                }
            }
            else
            {
                this.btnoff.IsEnabled = false;
            }
        }

        public delegate void afterCloseClick(object sender);
    }
}
