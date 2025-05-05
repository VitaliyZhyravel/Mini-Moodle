using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Models.Dto.GradeDto;
using Mini_Moodle.Repositories.GradeService.CreateGrade;

namespace Mini_Moodle.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class GradeController : ControllerBase
{
    private readonly IMediator mediator;

    public GradeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<GradeResponseDto>> Create([FromBody] GradeRequestDto userRequest) 
    {
        try
        {
            var query = new GradeCreateCommand(userRequest);
            var result = await mediator.Send(query);

            return result.IsSuccess ? Ok(result.Data): BadRequest(result.ErrorMessage);
        }
        catch (Exception ex )
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }
}
