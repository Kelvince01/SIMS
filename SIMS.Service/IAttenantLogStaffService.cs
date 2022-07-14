using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public interface IAttenantLogStaffService
    {
        IEnumerable<AttenantLogStaff> Gets(string name = null);

        AttenantLogStaff Get(Decimal id);

        AttenantLogStaff GetByStaffAndDate(string cardNo, DateTime date);

        void CreateUpdate(AttenantLogStaff model);

        void Update(AttenantLogStaff model);

        void Remove(AttenantLogStaff model);

        void Save();

        bool IsDuplicate(AttenantLogStaff model);

        IEnumerable<AttenantLogStaff> SelectRecordByStaffCode(
            string Barcode,
            int top);
    }
}
