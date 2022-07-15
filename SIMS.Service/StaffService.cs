using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class StaffService : IStaffService
    {
        public readonly IStaffRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public StaffService(IDbFactory idbFactory)
        {
            this._repository = (IStaffRepository)new StaffRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<Staff> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._repository.GetAll() : this._repository.GetAll().Where<Staff>((Func<Staff, bool>)(c => c.StaffName == name));

        public Staff Get(Decimal id) => this._repository.GetById(id);

        public Staff Get(string name) => (Staff)null;

        public void CreateUpdate(Staff model) => this._repository.Add(model);

        public void Update(Staff model) => this._repository.Update(model);

        public void Remove(Staff model) => this._repository.Delete(model);

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(Staff model) => false;

        public List<Staff> GetServerStaff(string shopId)
        {
            Result result = new SQLDAL("Server").Select("SELECT [StaffId],[CardNo]\r\n                                                  ,[StaffName],[Mobile],[Email]\r\n                                                  ,[Address],[DesignationId]\r\n                                                  ,[CampusId],IsActive\r\n                                              FROM [Staff]\r\n                                              WHERE [CampusId]='" + shopId + "'");
            if (!result.ExecutionState)
                return (List<Staff>)null;
            List<Staff> serverStaff = new List<Staff>();
            for (int index = 0; index < result.Data.Rows.Count; ++index)
                serverStaff.Add(new Staff()
                {
                    StaffId = int.Parse(result.Data.Rows[index]["StaffId"].ToString()),
                    CardNo = result.Data.Rows[index]["CardNo"].ToString(),
                    StaffName = result.Data.Rows[index]["StaffName"].ToString(),
                    Mobile = result.Data.Rows[index]["Mobile"].ToString(),
                    Email = result.Data.Rows[index]["Email"].ToString(),
                    Address = result.Data.Rows[index]["Address"].ToString(),
                    DesignationId = new int?(int.Parse(result.Data.Rows[index]["DesignationId"].ToString())),
                    CampusId = result.Data.Rows[index]["CampusId"].ToString(),
                    IsActive = result.Data.Rows[index]["IsActive"].ToString()
                });
            return serverStaff;
        }

        public Staff GetByCardNo(string cardNo) => this._repository.GetMany((Expression<Func<Staff, bool>>)(m => m.CardNo == cardNo && m.IsActive == "1")).FirstOrDefault<Staff>();

        public List<Staff> GetStaff(string DefaultText)
        {
            List<Staff> list = this._repository.GetAll().ToList<Staff>();
            list.Insert(0, new Staff()
            {
                StaffId = -1,
                StaffName = DefaultText
            });
            return list;
        }
    }
}
