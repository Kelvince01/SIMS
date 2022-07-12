using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for SplashScreenUI.xaml
    /// </summary>
    public partial class SplashScreenUI : MetroWindow
    {
        private DispatcherTimer timer1;

        public SplashScreenUI()
        {
            InitializeComponent();
            this.timer1.Start();
            this.timer1.IsEnabled = true;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.metroProgressSpinner1.Value == 100)
            {
                if (!StaticData.isAllDataLoaded)
                    return;
                this.timer1.Stop();
                this.timer1.IsEnabled = false;
                this.Hide();
                this.Close();
            }
            else
            {
                this.lblMsg.Content += ".";
                this.metroProgressSpinner1.Value += 5;
            }
        }
    }
}
