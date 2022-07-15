using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface ICampusService
    {
        IEnumerable<Campus> Gets(string name = null);

        Campus Get(Decimal id);

        Campus Get(string name);

        void CreateUpdate(Campus model);

        void Update(Campus model);

        void Remove(Campus model);

        void Save();

        bool IsDuplicate(Campus model);

        Campus GetById(string shopId);

        List<Campus> GetServerCampus();

        List<Campus> GetCampus(string defaultText);

        void RemoveAll();
    }
}
