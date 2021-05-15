using SampleDAL.DataAccess;
using SampleDAL.Repositories.Base;
using SampleDAL.Repositories.Interfaces;
using SampleModel.Entities;

namespace SampleDAL.Repositories
{
    public class AuthRepository : BaseRepository<Auth>, IAuthRepository
    {

        public AuthRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
