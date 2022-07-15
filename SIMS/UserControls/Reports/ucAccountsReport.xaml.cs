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
using SIMS.Service;

namespace SIMS.UserControls.Reports
{
    /// <summary>
    /// Interaction logic for ucAccountsReport.xaml
    /// </summary>
    public partial class ucAccountsReport : BaseUserControl
    {
        private IStyleSizeService _serviceStyleSize;

        public ucAccountsReport()
        {
            InitializeComponent();
            this.SetTheme();
        }

        public event ucAccountsReport.afterCloseClick onCloseClick;

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.onCloseClick == null)
                return;
            this.onCloseClick((object)this);
        }

        public delegate void afterCloseClick(object sender);
    }
}
