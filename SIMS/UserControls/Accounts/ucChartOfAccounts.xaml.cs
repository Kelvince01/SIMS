using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
using Label = System.Windows.Controls.Label;

namespace SIMS.UserControls.Accounts
{
    /// <summary>
    /// Interaction logic for ucChartOfAccounts.xaml
    /// </summary>
    public partial class ucChartOfAccounts : BaseUserControl
    {
        private IAct_MasterChartOfAccountService _serviceChartOfAccount;
        private bool _thisLevel;
        private List<Act_MasterChartOfAccount> AccountsList;
        private string operationMode = "";
        private TreeNode _selectedNode = (TreeNode)null;

        public ucChartOfAccounts()
        {
            InitializeComponent();
            this.Init();
            this.SetTheme();
        }

        private void Init() => this._serviceChartOfAccount = (IAct_MasterChartOfAccountService)new Act_MasterChartOfAccountService((IDbFactory)new DbFactory());

        public event ucChartOfAccounts.afterCloseClick onCloseClick;

        private void LoadParentHeader(Act_MasterChartOfAccount masterChartOfAcc)
        {
            this.lblPrents.Content = "";
            if (!masterChartOfAcc.ParentID.HasValue)
                return;
            List<Act_MasterChartOfAccount> list = Enumerable.OrderBy<Act_MasterChartOfAccount, Decimal?>((IEnumerable<Act_MasterChartOfAccount>)this._serviceChartOfAccount.GetAllParent(masterChartOfAcc.ParentID.Value), (Func<Act_MasterChartOfAccount, Decimal?>)(m => m.ID)).ToList<Act_MasterChartOfAccount>();
            int count = 1;
            foreach (Act_MasterChartOfAccount masterChartOfAccount in list)
            {
                Label lblPrents = this.lblPrents;
                lblPrents.Content = lblPrents.Content + this.AddChildString(count) + masterChartOfAccount.ActName + "-" + (object)masterChartOfAccount.ActCode;
                this.lblPrents.Content += "\r\n";
                ++count;
            }
        }

        public string GetNewChartOfAccountCode(Act_MasterChartOfAccount masterChartOfAcc)
        {
            string rightStringLength = "";
            string initialValue = "";
            if (masterChartOfAcc.Level == 2)
            {
                rightStringLength = "2";
                initialValue = "01";
            }
            else if (masterChartOfAcc.Level == 3)
            {
                rightStringLength = "3";
                initialValue = "001";
            }
            else if (masterChartOfAcc.Level == 4)
            {
                rightStringLength = "6";
                initialValue = "000001";
            }
            return new GlobalClass().GetMaxIdAccountCode("ActCode", rightStringLength, initialValue, "Act_MasterChartOfAccount", masterChartOfAcc.ActCode.ToString(), masterChartOfAcc.ID.ToString());
        }

        private string AddChildString(int count)
        {
            string str = "";
            for (int index = 0; index < count; ++index)
                str += "..";
            return str;
        }

        public delegate void afterCloseClick(object sender);
    }
}
