using SampleDAL.DataAccess;
using SampleDAL.Repositories.Base;
using SampleDAL.Repositories.Interfaces;
using SampleModel.Entities;

namespace SampleDAL.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext db) : base(db) { }
    }
}
