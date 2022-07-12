using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class Act_MasterChartOfAccountService : IAct_MasterChartOfAccountService
    {
        public readonly IAct_MasterChartOfAccountRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public readonly ITempAccountChartManageRepository _repositoryTempAccountManage;

        public Act_MasterChartOfAccountService(IDbFactory idbFactory)
        {
            this._repository = (IAct_MasterChartOfAccountRepository)new Act_MasterChartOfAccountRepository(idbFactory);
            this._repositoryTempAccountManage = (ITempAccountChartManageRepository)new TempAccountChartManageRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<Act_MasterChartOfAccount> Gets(string name = null) => this._repository.GetAll();

        public Act_MasterChartOfAccount Get(string id)
        {
            Decimal _id = Decimal.Parse(id);
            return this._repository.GetMany((Expression<Func<Act_MasterChartOfAccount, bool>>)(m => m.ID == (Decimal?)_id)).FirstOrDefault<Act_MasterChartOfAccount>();
        }

        public Act_MasterChartOfAccount GetByChartId(string id)
        {
            Decimal _id = Decimal.Parse(id);
            return this._repository.GetMany((Expression<Func<Act_MasterChartOfAccount, bool>>)(m => m.ChartID == _id)).FirstOrDefault<Act_MasterChartOfAccount>();
        }

        public Act_MasterChartOfAccount GetByActCode(string id)
        {
            Decimal _id = Decimal.Parse(id);
            return this._repository.GetMany((Expression<Func<Act_MasterChartOfAccount, bool>>)(m => m.ActCode == _id)).FirstOrDefault<Act_MasterChartOfAccount>();
        }

        public void Create(Act_MasterChartOfAccount model) => this._repository.Add(model);

        public void Update(Act_MasterChartOfAccount model) => this._repository.Update(model);

        public void Remove(Act_MasterChartOfAccount model) => this._repository.Delete(model);

        public void RemoveAll() => this._repository.Delete((Expression<Func<Act_MasterChartOfAccount, bool>>)(m => (Decimal?)m.ActCode != new Decimal?()));

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(Act_MasterChartOfAccount model) => throw new NotImplementedException();

        public string[] GetServerFiscalYear()
        {
            Result result = new SQLDAL("Server").Select("SELECT ID ,CompanyID ,SYear ,EYear ,FiscalYear \r\n                                                        FROM Act_CompanyFiscalYear\r\n                                                        WHERE FiscalYear=\r\n                                                        (SELECT MAX(FiscalYear) FROM Act_CompanyFiscalYear)");
            if (!result.ExecutionState)
                return (string[])null;
            string[] serverFiscalYear = new string[2];
            for (int index = 0; index < result.Data.Rows.Count; ++index)
            {
                serverFiscalYear[0] = result.Data.Rows[index]["SYear"].ToString();
                serverFiscalYear[1] = result.Data.Rows[index]["EYear"].ToString();
            }
            return serverFiscalYear;
        }

        public List<Act_MasterChartOfAccount> GetServerChartOfAccount()
        {
            Result result = new SQLDAL("Server").Select("SELECT [ChartID],[CompanyID],[ActCode],[ActName],[Level]\r\n                                                              ,[ParentID],[ID],[FiscalYear],[IsTransactional]\r\n                                                              ,[IsControl]\r\n                                                        FROM [Act_MasterChartOfAccount]");
            if (!result.ExecutionState)
                return (List<Act_MasterChartOfAccount>)null;
            List<Act_MasterChartOfAccount> serverChartOfAccount = new List<Act_MasterChartOfAccount>();
            for (int index = 0; index < result.Data.Rows.Count; ++index)
            {
                Act_MasterChartOfAccount masterChartOfAccount = new Act_MasterChartOfAccount();
                masterChartOfAccount.ChartID = Decimal.Parse(result.Data.Rows[index]["ChartID"].ToString());
                masterChartOfAccount.CompanyID = Decimal.Parse(result.Data.Rows[index]["CompanyID"].ToString());
                masterChartOfAccount.ActCode = Decimal.Parse(result.Data.Rows[index]["ActCode"].ToString());
                masterChartOfAccount.ActName = result.Data.Rows[index]["ActName"].ToString();
                masterChartOfAccount.Level = int.Parse(result.Data.Rows[index]["Level"].ToString());
                if (result.Data.Rows[index]["ParentID"].ToString() != "")
                    masterChartOfAccount.ParentID = new Decimal?(Decimal.Parse(result.Data.Rows[index]["ParentID"].ToString()));
                masterChartOfAccount.ID = new Decimal?(Decimal.Parse(result.Data.Rows[index]["ID"].ToString()));
                masterChartOfAccount.FiscalYear = new int?(int.Parse(result.Data.Rows[index]["FiscalYear"].ToString()));
                masterChartOfAccount.IsTransactional = result.Data.Rows[index]["IsTransactional"].ToString().Trim();
                masterChartOfAccount.IsControl = result.Data.Rows[index]["IsControl"].ToString();
                serverChartOfAccount.Add(masterChartOfAccount);
            }
            return serverChartOfAccount;
        }

        public List<Act_MasterChartOfAccount> GetCurrentYearChartOfAccount()
        {
            Result result = new SQLDAL().Select("SELECT * FROM dbo.Act_MasterChartOfAccount\r\n                            WHERE (\r\n                            LEFT(ActCode, LEN((SELECT BankCode FROM dbo.GlobalSetup))  )= (SELECT BankCode FROM dbo.GlobalSetup)\r\n                            OR LEFT(ActCode, LEN((SELECT ExpenseCode FROM dbo.GlobalSetup))  )= (SELECT ExpenseCode FROM dbo.GlobalSetup)\r\n                            ) \r\n\r\n                            AND FiscalYear=(SELECT DATEPART(YEAR,FiscYr2) FROM dbo.GlobalSetup)\r\n                            AND ISNULL(IsControl,'N')<>'Y'\r\n\r\n                            ORDER BY ActCode\r\n\r\n                            ");
            if (!result.ExecutionState)
                return (List<Act_MasterChartOfAccount>)null;
            List<Act_MasterChartOfAccount> yearChartOfAccount = new List<Act_MasterChartOfAccount>();
            for (int index = 0; index < result.Data.Rows.Count; ++index)
            {
                Act_MasterChartOfAccount masterChartOfAccount = new Act_MasterChartOfAccount();
                masterChartOfAccount.ChartID = Decimal.Parse(result.Data.Rows[index]["ChartID"].ToString());
                masterChartOfAccount.CompanyID = Decimal.Parse(result.Data.Rows[index]["CompanyID"].ToString());
                masterChartOfAccount.ActCode = Decimal.Parse(result.Data.Rows[index]["ActCode"].ToString());
                masterChartOfAccount.ActName = result.Data.Rows[index]["ActName"].ToString();
                masterChartOfAccount.Level = int.Parse(result.Data.Rows[index]["Level"].ToString());
                if (result.Data.Rows[index]["ParentID"].ToString() != "")
                    masterChartOfAccount.ParentID = new Decimal?(Decimal.Parse(result.Data.Rows[index]["ParentID"].ToString()));
                masterChartOfAccount.ID = new Decimal?(Decimal.Parse(result.Data.Rows[index]["ID"].ToString()));
                masterChartOfAccount.FiscalYear = new int?(int.Parse(result.Data.Rows[index]["FiscalYear"].ToString()));
                masterChartOfAccount.IsTransactional = result.Data.Rows[index]["IsTransactional"].ToString();
                masterChartOfAccount.IsControl = result.Data.Rows[index]["IsControl"].ToString();
                yearChartOfAccount.Add(masterChartOfAccount);
            }
            return yearChartOfAccount;
        }

        public List<Act_MasterChartOfAccount> GetCurrentYearChartOfAccountWithOutBankCode()
        {
            Result result = new SQLDAL().Select("SELECT * FROM dbo.Act_MasterChartOfAccount\r\n                            WHERE (\r\n                             LEFT(ActCode, LEN((SELECT ExpenseCode FROM dbo.GlobalSetup))  )= (SELECT ExpenseCode FROM dbo.GlobalSetup)\r\n                            ) \r\n\r\n                            AND FiscalYear=(SELECT DATEPART(YEAR,FiscYr2) FROM dbo.GlobalSetup)\r\n                            AND ISNULL(IsControl,'N')<>'Y'\r\n\r\n                            ORDER BY ActCode\r\n\r\n                            ");
            if (!result.ExecutionState)
                return (List<Act_MasterChartOfAccount>)null;
            List<Act_MasterChartOfAccount> accountWithOutBankCode = new List<Act_MasterChartOfAccount>();
            for (int index = 0; index < result.Data.Rows.Count; ++index)
            {
                Act_MasterChartOfAccount masterChartOfAccount = new Act_MasterChartOfAccount();
                masterChartOfAccount.ChartID = Decimal.Parse(result.Data.Rows[index]["ChartID"].ToString());
                masterChartOfAccount.CompanyID = Decimal.Parse(result.Data.Rows[index]["CompanyID"].ToString());
                masterChartOfAccount.ActCode = Decimal.Parse(result.Data.Rows[index]["ActCode"].ToString());
                masterChartOfAccount.ActName = result.Data.Rows[index]["ActName"].ToString();
                masterChartOfAccount.Level = int.Parse(result.Data.Rows[index]["Level"].ToString());
                if (result.Data.Rows[index]["ParentID"].ToString() != "")
                    masterChartOfAccount.ParentID = new Decimal?(Decimal.Parse(result.Data.Rows[index]["ParentID"].ToString()));
                masterChartOfAccount.ID = new Decimal?(Decimal.Parse(result.Data.Rows[index]["ID"].ToString()));
                masterChartOfAccount.FiscalYear = new int?(int.Parse(result.Data.Rows[index]["FiscalYear"].ToString()));
                masterChartOfAccount.IsTransactional = result.Data.Rows[index]["IsTransactional"].ToString();
                masterChartOfAccount.IsControl = result.Data.Rows[index]["IsControl"].ToString();
                accountWithOutBankCode.Add(masterChartOfAccount);
            }
            return accountWithOutBankCode;
        }

        public List<Act_MasterChartOfAccount> GetAllParent(Decimal ParentID)
        {
            List<Act_MasterChartOfAccount> allParent = new List<Act_MasterChartOfAccount>();
            while (true)
            {
                Act_MasterChartOfAccount masterChartOfAccount = this._repository.GetMany((Expression<Func<Act_MasterChartOfAccount, bool>>)(m => m.ID == (Decimal?)ParentID)).FirstOrDefault<Act_MasterChartOfAccount>();
                if (masterChartOfAccount != null)
                {
                    allParent.Add(masterChartOfAccount);
                    Decimal? parentId = masterChartOfAccount.ParentID;
                    if (parentId.HasValue)
                    {
                        parentId = masterChartOfAccount.ParentID;
                        ParentID = parentId.Value;
                    }
                    else
                        break;
                }
                else
                    break;
            }
            return allParent;
        }

        public List<Act_MasterChartOfAccount> GetAll()
        {
            List<Act_MasterChartOfAccount> masterChartOfAccountList = new List<Act_MasterChartOfAccount>();
            return this._repository.GetAll().ToList<Act_MasterChartOfAccount>();
        }

        public List<Act_MasterChartOfAccount> GetChilds(Decimal id)
        {
            List<Act_MasterChartOfAccount> masterChartOfAccountList = new List<Act_MasterChartOfAccount>();
            return this._repository.GetMany((Expression<Func<Act_MasterChartOfAccount, bool>>)(m => m.ParentID == (Decimal?)id)).ToList<Act_MasterChartOfAccount>();
        }

        public List<Act_MasterChartOfAccount> GetAccountByName(
          string searchText)
        {
            List<Act_MasterChartOfAccount> accountByName = new List<Act_MasterChartOfAccount>();
            Result result = new SQLDAL().Select("SELECT ChartID ,CompanyID ,ActCode ,ActName ,\r\n                                    Level ,ParentID ,ID ,FiscalYear ,\r\n                                    IsTransactional ,IsControl \r\n                            FROM dbo.Act_MasterChartOfAccount\r\n                            WHERE ActCode LIKE '%" + searchText + "%' OR ActName LIKE '%" + searchText + "%'  ");
            if (!result.ExecutionState)
                return (List<Act_MasterChartOfAccount>)null;
            for (int index = 0; index < result.Data.Rows.Count; ++index)
            {
                Act_MasterChartOfAccount masterChartOfAccount = new Act_MasterChartOfAccount();
                masterChartOfAccount.ChartID = Decimal.Parse(result.Data.Rows[index]["ChartID"].ToString());
                masterChartOfAccount.CompanyID = Decimal.Parse(result.Data.Rows[index]["CompanyID"].ToString());
                masterChartOfAccount.ActCode = Decimal.Parse(result.Data.Rows[index]["ActCode"].ToString());
                masterChartOfAccount.ActName = result.Data.Rows[index]["ActName"].ToString();
                masterChartOfAccount.Level = int.Parse(result.Data.Rows[index]["Level"].ToString());
                if (result.Data.Rows[index]["ParentID"].ToString() != "")
                    masterChartOfAccount.ParentID = new Decimal?(Decimal.Parse(result.Data.Rows[index]["ParentID"].ToString()));
                masterChartOfAccount.ID = new Decimal?(Decimal.Parse(result.Data.Rows[index]["ID"].ToString()));
                masterChartOfAccount.FiscalYear = new int?(int.Parse(result.Data.Rows[index]["FiscalYear"].ToString()));
                masterChartOfAccount.IsTransactional = result.Data.Rows[index]["IsTransactional"].ToString();
                masterChartOfAccount.IsControl = result.Data.Rows[index]["IsControl"].ToString();
                accountByName.Add(masterChartOfAccount);
            }
            return accountByName;
        }

        public void CreateTemp(TempAccountChartManage model) => this._repositoryTempAccountManage.Add(model);

        public void RemoveTemp(string userId) => this._repositoryTempAccountManage.Delete((Expression<Func<TempAccountChartManage, bool>>)(m => m.UserId == userId));

        public TempAccountChartManage GetTempOne() => this._repositoryTempAccountManage.GetAll().FirstOrDefault<TempAccountChartManage>();

        public List<TempAccountChartManage> GetTemp()
        {
            List<TempAccountChartManage> accountChartManageList = new List<TempAccountChartManage>();
            return this._repositoryTempAccountManage.GetAll().ToList<TempAccountChartManage>();
        }
    }
}
