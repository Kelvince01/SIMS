using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IStaffService
    {
        IEnumerable<Staff> Gets(string name = null);

        Staff Get(Decimal id);

        Staff Get(string name);

        void CreateUpdate(Staff model);

        void Update(Staff model);

        void Remove(Staff model);

        void Save();

        bool IsDuplicate(Staff model);

        Staff GetByCardNo(string cardNo);

        List<Staff> GetServerStaff(string shopId);

        List<Staff> GetStaff(string DefaultText);
    }
}
