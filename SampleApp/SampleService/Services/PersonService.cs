using Microsoft.Extensions.Configuration;
using SampleDAL.Repositories.Interfaces;
using SampleModel.Entities;
using SampleService.Services.Base;
using SampleService.Services.Interfaces;

namespace SampleService.Services
{
    public class PersonService : BaseService<Person>, IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IConfiguration _config;

        public PersonService
        (
            IPersonRepository personRepository,
            IConfiguration config
        ) : base(personRepository)
        {
            _personRepository = personRepository;
            _config = config;
        }
    }
}
