using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Get(User user);
        void Create(User user);
    }
}
