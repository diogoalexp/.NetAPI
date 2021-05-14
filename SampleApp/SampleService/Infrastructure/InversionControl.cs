using SampleDAL;
using SampleDAL.Repositories;
using SampleDAL.Repositories.Base;
using SampleDAL.Repositories.Interfaces;
using SampleService.Services;
using SampleService.Services.Base;
using SampleService.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace SampleService
{
    public static class InversionControl
    {
        public static Dictionary<Type, Type> GetDomainTypes()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(IBaseService<>), typeof(BaseService<>) },
                { typeof(IUserService), typeof(UserService) },
                { typeof(IPersonService), typeof(PersonService) },
            };
        }

        public static Dictionary<Type, Type> GetDataTypes()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(IBaseRepository<>), typeof(BaseRepository<>) },                
                { typeof(IUserRepository), typeof(UserRepository) },
                { typeof(IPersonRepository), typeof(PersonRepository) },
            };
        }
    }
}
