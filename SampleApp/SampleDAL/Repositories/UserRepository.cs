using SampleDAL.DataAccess;
using SampleDAL.Repositories.Base;
using SampleDAL.Repositories.Interfaces;
using SampleModel.Entities;

namespace SampleDAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
