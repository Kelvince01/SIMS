using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IUsersDesktopService
    {
        IEnumerable<UsersDesktop> Gets(string name = null);

        UsersDesktop Get(Decimal id);

        UsersDesktop Get(string name);

        void Create(UsersDesktop model);

        void Update(UsersDesktop model);

        void Remove(UsersDesktop model);

        void Save();

        bool IsDuplicate(UsersDesktop model, bool isNew);

        UsersDesktop IsValidUsers(string userName, string password);
    }
}
