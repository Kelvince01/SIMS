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
using System.Windows.Shapes;

namespace SIMS.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void cbFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
        }

        private void mNewRecord_Click(object sender, RoutedEventArgs e)
        {
        }

        private void myReport_Click(object sender, RoutedEventArgs e)
        {
        }

        private void mOpportunity_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SalesMaint_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Contracts_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Finance_Click(object sender, RoutedEventArgs e)
        {
        }

        private void TechLead_Click(object sender, RoutedEventArgs e)
        {
        }

        private void PmgrMaint_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ProposalMgr_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CaptureMgr_Click(object sender, RoutedEventArgs e)
        {
        }

        private void mCustomer_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window1_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // calculates incorrect when window is maximized
            ContactsGrid.Width = this.ActualWidth - 20;
            ContactsGrid.Height = this.ActualHeight - 190;
        }
    }
}
