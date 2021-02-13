using SampleDAL;
using SampleDAL.Repositories;
using SampleDAL.Repositories.Interfaces;
using SampleModel.Entities;
using SampleService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleService
{
    public static class InversionControl
    {
        public static Dictionary<Type, Type> GetDomainTypes()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(IBaseService<>), typeof(BaseService<>) },
                { typeof(IUserService), typeof(UserService) }
            };
        }

        public static Dictionary<Type, Type> GetDataTypes()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(IBaseRepository<>), typeof(BaseRepository<>) },
                { typeof(IPersonRepository), typeof(PersonRepository) },
                { typeof(IUserRepository), typeof(UserRepository) },
            };
        }
    }
}
