using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IDbFactory idbFactory)
        {
            this._personRepository = (IPersonRepository)new PersonRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<Person> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._personRepository.GetAll() : this._personRepository.GetAll().Where<Person>((Func<Person, bool>)(c => c.PersonID.ToString() == name));

        public Person Get(decimal id)
        {
            return this._personRepository.GetById(id);
        }

        public Person GetByPersonAndDate(int personId, DateTime date)
        {
            return this._personRepository.GetMany((Expression<Func<Person, bool>>)(m => m.PersonID == personId && DbFunctions.TruncateTime((DateTime?)m.HireDate.Value) == DbFunctions.TruncateTime((DateTime?)date))).FirstOrDefault<Person>();
        }

        public void CreateUpdate(Person model)
        {
            Person person = this._personRepository.GetMany((Expression<Func<Person, bool>>)(m => m.PersonID == model.PersonID)).FirstOrDefault<Person>();
            if (person != null)
            {
                person.EnrollmentDate = model.EnrollmentDate;
                person.HireDate = model.HireDate;
            }
            else
                this._personRepository.Add(model);
        }

        public void Update(Person model)
        {
            this._personRepository.Update(model);
        }

        public void Remove(Person model)
        {
            this._personRepository.Delete(model);
        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }

        public bool IsDuplicate(Person model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> SelectRecordByPersonId(int personId, int top)
        {
            return this._personRepository
                .GetMany((Expression<Func<Person, bool>>)(m => m.PersonID == personId))
                .OrderByDescending<Person, Decimal>((Func<Person, Decimal>)(m => m.PersonID))
                .Take<Person>(top);
        }
    }
}
