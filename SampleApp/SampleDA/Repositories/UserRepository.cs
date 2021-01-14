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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public User Get(User user)
        {            
            return _db.User.Where(item => item.Login == user.Login).FirstOrDefault();
        }

        public void Create(User user)
        {
            _db.User.Add(user);
            _db.SaveChanges();
        }
    }
}
