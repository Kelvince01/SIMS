using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IPersonService
    {
        IEnumerable<Person> Gets(string name = null);

        Person Get(Decimal id);

        Person GetByPersonAndDate(int personId, DateTime date);

        void CreateUpdate(Person model);

        void Update(Person model);

        void Remove(Person model);

        void Save();

        bool IsDuplicate(Person model);

        IEnumerable<Person> SelectRecordByPersonId(
            int personId,
            int top);
    }
}
