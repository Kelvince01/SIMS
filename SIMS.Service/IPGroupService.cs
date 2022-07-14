using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IPGroupService
    {
        IEnumerable<PGroup> Gets(string name = null);

        PGroup Get(Decimal id);

        PGroup Get(string name);

        void Create(PGroup model);

        void Update(PGroup model);

        void Remove(PGroup model);

        void Save();

        bool IsDuplicate(PGroup model);

        PGroup GetById(string PGroupId);
    }
}
