using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IAttenantLogService
    {
        IEnumerable<AttenantLog> Gets(string name = null);

        AttenantLog Get(Decimal id);

        AttenantLog GetByShopAndDate(string name, DateTime date);

        void Create(AttenantLog model);

        void Update(AttenantLog model);

        void Remove(AttenantLog model);

        void Save();

        bool IsDuplicate(AttenantLog model);
    }
}
