using Microsoft.Extensions.Configuration;
using SampleDAL.Repositories.Interfaces;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleService
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

        public async Task<AuthResponse> Authenticate(AuthRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Login))
                throw new Exception("Informe o login.");

            var user = (await _userRepository.GetAsync(model.asDomain(), x => x.Login == model.Login)).FirstOrDefault();

            // Verifica se o usuário existe
            if (user == null)
                throw new Exception("Usuário inválido");

            if (user.Pass != model.Password)
                throw new Exception("Senha inválida");

            // Gera o Token
            var token = TokenService.GenerateToken(user, _config["Secret"]);

            return user.asAuthResponse(token);
        }

        public async Task<RegisterResponse> Register(AuthRequest model)
        {
            var user = (await _userRepository.GetAsync(model.asDomain(), x => x.Login == model.Login)).FirstOrDefault();

            if (user is not null)
                throw new Exception("Usuário já existe");

            user = await _userRepository.InsertAsync(model.asDomain());

            return user.asRegisterResponse();
        }

    }
}
