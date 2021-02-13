using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SampleDAL.Repositories;
using SampleDAL.Repositories.Interfaces;
using SampleModel;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleService;
using SampleService.Interfaces;
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

        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserRepository repository, IConfiguration config, IUserService userService, ILogger<AuthController> logger)
        {
            this.userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] AuthRequest model)
        {
            try
            {
                return Ok(await userService.Authenticate(model));
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex, model));
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] AuthRequest model)
        {
            try
            {
                return Ok(await userService.Register(model));
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex, model));
            }
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
