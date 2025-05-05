using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Models.Dto.AssignmentDtos;
using Mini_Moodle.Repositories.UserService.Queries.GetAllUserSubmissions;

namespace Mini_Moodle.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("MyAssignment")]
    public async Task<ActionResult<AssignmentResponseForUserDto>> GetUserAssignments()
    {
        try
        {
            var query = new UserGetSubmissionQuery();
            var result = await mediator.Send(query);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }
}
