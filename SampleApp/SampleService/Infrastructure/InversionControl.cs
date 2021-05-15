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
                { typeof(IAuthService), typeof(AuthService) },
                { typeof(IPersonService), typeof(PersonService) },
            };
        }

        public static Dictionary<Type, Type> GetDataTypes()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(IBaseRepository<>), typeof(BaseRepository<>) },                
                { typeof(IAuthRepository), typeof(AuthRepository) },
                { typeof(IPersonRepository), typeof(PersonRepository) },
            };
        }
    }
}
