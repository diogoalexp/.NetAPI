using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleDAL.Repositories;
using SampleDAL.Repositories.Interfaces;
using SampleModel;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserRepository repository;
        private readonly IConfiguration config;

        public AuthController(IUserRepository repository, IConfiguration config)
        {
            this.repository = repository;
            this.config = config;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate([FromBody] UserDTO model)
        {
            User userParam = new()
            {
                Login = model.Login,
                Pass = model.Password
            };

            // Recupera o usuário
            var user = repository.Get(userParam);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário inválido" });

            if (user.Password != userParam.Password)
                return NotFound(new { message = "Senha inválida" });

            // Gera o Token
            var token = TokenService.GenerateToken(user, config["Secret"]);

            // Retorna os dados
            return new
            {
                user = new UserResponseDTO() {Login = user.Login, Role = user.Role, Id = user.Id },
                token = token
            };
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<UserResponseDTO> Register([FromBody] UserDTO model)
        {
            User user = new()
            {
                Login = model.Login,
                Pass = model.Password,
                Role = SampleModel.Enum.Roles.Normal,
            };

            if (repository.Get(user) is not null)
                return NotFound(new { message = "Usuário já existe" });

            repository.Create(user);

            return Ok(user.asDto());
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "Normal,Admin")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "Admin")]
        public string Manager() => "Gerente";

        
    }
}
