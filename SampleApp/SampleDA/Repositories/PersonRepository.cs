using SampleDAL.DataAccess;
using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<Person> GetPersons()
        {
            return _db.Person.ToList();
        }

        public Person GetPerson(int id)
        {
            return _db.Person.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreatePerson(Person person)
        {
            _db.Person.Add(person);
            _db.SaveChanges();
        }

        public void UpdatePerson(Person person)
        {
            _db.Person.Update(person);
            _db.SaveChanges();
        }

        public void DeletePerson(int id)
        {
            var person = _db.Person.Find(id);

            _db.Person.Remove(person);
            _db.SaveChanges();
        }

    }
}
