using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDAL.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();
        Person GetPerson(int id);
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(int id);
    }
}
