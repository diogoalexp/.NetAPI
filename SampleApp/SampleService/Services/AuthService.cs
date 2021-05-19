using AutoMapper;
using Microsoft.Extensions.Configuration;
using SampleDAL.Repositories.Interfaces;
using SampleModel.Constants;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleService.Services.Base;
using SampleService.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SampleService.Services
{
    public class AuthService : BaseService<Auth>, IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService
        (
            IAuthRepository userRepository,
            IConfiguration config, 
            IMapper mapper
        ) : base(userRepository)
        {
            _config = config;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> SignIn(AuthRequestDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.Login))
                throw new Exception(Constants.Error.INVALID_LOGIN);

            var user = (await GetAsync(_mapper.Map<Auth>(model), x => x.Login == model.Login)).FirstOrDefault();

            if (user == null)
                throw new Exception(Constants.Error.INVALID_USER);

            if (user.Password != model.Password)
                throw new Exception(Constants.Error.INVALID_PASSWORD);

            var authResponse = _mapper.Map<AuthResponseDTO>(user);

            authResponse.Token = TokenService.GenerateToken(user, _config["Secret"]);

            return authResponse;
        }

        public async Task<RegisterResponseDTO> SignUp(AuthRequestDTO model)
        {
            var user = (await GetAsync(_mapper.Map<Auth>(model), x => x.Login == model.Login)).FirstOrDefault();

            if (user is not null)
                throw new Exception(Constants.Error.INVALID_USER_EXISTS);

            user = await InsertAsync(_mapper.Map<Auth>(model));

            return _mapper.Map<RegisterResponseDTO>(user);
        }

    }
}
