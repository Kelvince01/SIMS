using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IDesktopMenuService
    {
        IEnumerable<DesktopMenu> Gets(string name = null);

        DesktopMenu Get(Decimal id);

        void Create(DesktopMenu model);

        void Update(DesktopMenu model);

        void Remove(DesktopMenu model);

        void Save();

        bool IsDuplicate(DesktopMenu model);

        List<DesktopMenu> SelectAllParentMenu();
    }
}
