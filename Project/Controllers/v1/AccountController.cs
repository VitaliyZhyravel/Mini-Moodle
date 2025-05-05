using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Models.Dto.Account;
using Mini_Moodle.Repositories.AccountService.Command.Login;
using Mini_Moodle.Repositories.AccountService.Command.Register;

namespace Mini_Moodle.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto userRequest)
        {
            try
            {
                var query = new AccountRegisterCommand(userRequest);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto userRequest)
        {
            try
            {
                var query = new AccountLoginCommand(userRequest);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("Some-Token");
                return Ok("Logout successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }
    }
}
