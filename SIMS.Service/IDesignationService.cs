using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IDesignationService
    {
        IEnumerable<Designation> Gets(string name = null);

        Designation Get(Decimal id);

        Designation Get(string name);

        void CreateUpdate(Designation model);

        void Update(Designation model);

        void Remove(Designation model);

        void Save();

        bool IsDuplicate(Designation model);

        List<Designation> GetServerDesignation();
    }
}
