using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IAct_MasterChartOfAccountService
    {
        void CreateTemp(TempAccountChartManage model);

        void RemoveTemp(string userId);

        List<TempAccountChartManage> GetTemp();

        TempAccountChartManage GetTempOne();

        IEnumerable<Act_MasterChartOfAccount> Gets(string name = null);

        Act_MasterChartOfAccount Get(string id);

        void Create(Act_MasterChartOfAccount model);

        void Update(Act_MasterChartOfAccount model);

        void Remove(Act_MasterChartOfAccount model);

        void Save();

        bool IsDuplicate(Act_MasterChartOfAccount model);

        List<Act_MasterChartOfAccount> GetServerChartOfAccount();

        Act_MasterChartOfAccount GetByChartId(string id);

        string[] GetServerFiscalYear();

        List<Act_MasterChartOfAccount> GetAccountByName(string searchText);

        List<Act_MasterChartOfAccount> GetCurrentYearChartOfAccountWithOutBankCode();

        List<Act_MasterChartOfAccount> GetCurrentYearChartOfAccount();

        List<Act_MasterChartOfAccount> GetAllParent(Decimal ParentID);

        void RemoveAll();

        List<Act_MasterChartOfAccount> GetAll();

        List<Act_MasterChartOfAccount> GetChilds(Decimal id);

        Act_MasterChartOfAccount GetByActCode(string id);
    }
}
