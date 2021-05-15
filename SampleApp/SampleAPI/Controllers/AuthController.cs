using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SampleModel.DTO;
using SampleModel.Helper;
using SampleService.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration config, IAuthService userService, ILogger<AuthController> logger)
        {
            this.userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("signIn")]
        public async Task<ActionResult<dynamic>> SignIn([FromBody] AuthRequestDTO model)
        {
            try
            {
                return Ok(await userService.SignIn(model));
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex, model));
            }
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<ActionResult<RegisterResponseDTO>> SignUp([FromBody] AuthRequestDTO model)
        {
            try
            {
                return Ok(await userService.SignUp(model));
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
        public string Authenticated() => String.Format("Authenticated - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "Normal,Admin")]
        public string Employee() => "Employee";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "Admin")]
        public string Manager() => "Manager";
    }
}
