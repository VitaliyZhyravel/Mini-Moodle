using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Models.Dto;
using Mini_Moodle.Repositories.Accounts.Command;

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
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                var query = new AccountRegisterCommand(request);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Registered {ex.InnerException} ");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var query = new AccountLoginCommand(request);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(($"An error occurred while logging in {ex.InnerException?.Message}"));
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
                return BadRequest($"An error occurred while logging out  {ex.InnerException?.Message}");
            }
        }
    }
}
