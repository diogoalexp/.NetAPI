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

        public AuthController(IAuthService userService, ILogger<AuthController> logger)
        {
            this.userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("signIn")]
        public async Task<ActionResult<AuthResponseDTO>> SignIn([FromBody] AuthRequestDTO model)
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
        public ActionResult<string> Anonymous() => Ok("Anonymous");

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public ActionResult<string> Authenticated() => Ok(String.Format("Authenticated - {0}", User.Identity.Name));

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "Normal,Admin")]
        public ActionResult<string> Employee() => Ok("Employee");

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> Manager() => Ok("Manager");
    }
}
