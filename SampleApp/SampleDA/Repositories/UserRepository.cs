using SampleDAL.DataAccess;
using SampleDAL.Repositories.Interfaces;
using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
