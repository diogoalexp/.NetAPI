using Microsoft.Extensions.Configuration;
using SampleDAL.Repositories.Interfaces;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleService.Services.Base;
using SampleService.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SampleService.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService
        (
            IUserRepository userRepository,
            IConfiguration config
        ) : base(userRepository)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<AuthResponseDTO> SignIn(AuthRequestDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.Login))
                throw new Exception("Login Incorrect.");

            var user = (await _userRepository.GetAsync(model.asDomain(), x => x.Login == model.Login)).FirstOrDefault();

            if (user == null)
                throw new Exception("Invalid user.");

            if (user.Pass != model.Password)
                throw new Exception("Invalid password.");

            var token = TokenService.GenerateToken(user, _config["Secret"]);

            return user.asAuthResponse(token);
        }

        public async Task<RegisterResponseDTO> SignUp(AuthRequestDTO model)
        {
            var user = (await _userRepository.GetAsync(model.asDomain(), x => x.Login == model.Login)).FirstOrDefault();

            if (user is not null)
                throw new Exception("User already exists");

            user = await _userRepository.InsertAsync(model.asDomain());

            return user.asRegisterResponse();
        }

    }
}
