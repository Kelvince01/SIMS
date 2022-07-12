using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IUsersDesktopMenusService
    {
        IEnumerable<UsersDesktopMenu> Gets(string name = null);

        UsersDesktopMenu Get(Decimal id);

        void Create(UsersDesktopMenu model);

        void Update(UsersDesktopMenu model);

        void Remove(UsersDesktopMenu model);

        void Save();

        bool IsDuplicate(UsersDesktopMenu model);
    }
}
