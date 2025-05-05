using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Models.Dto.AssignmentDto;
using Mini_Moodle.Models.Dto.AssignmentDtos;
using Mini_Moodle.Repositories.AssignmentsService.Commands.CreateAssignment;
using Mini_Moodle.Repositories.AssignmentsService.Commands.DeleteAssignment;
using Mini_Moodle.Repositories.AssignmentsService.Commands.UpdateAssignment;
using Mini_Moodle.Repositories.AssignmentsService.Queries.GetAllAssignment;
using Mini_Moodle.Repositories.AssignmentsService.Queries.GetByIdAssignment;

namespace Mini_Moodle.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AssignmentController : ControllerBase
{
    private readonly IMediator mediator;

    public AssignmentController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<AssignmentResponseDto>> GetAll([FromQuery] string? filterBy = null, [FromQuery] string? filterOn = null,
        [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true, int pageNumber = 0, int pageSize = 1000)
    {
        try
        {
            var query = new AssignmentGetAllQuery(filterBy, filterOn, sortBy, isAscending, pageNumber, pageSize);
            var result = await mediator.Send(query);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }

    [HttpGet("{Id:guid}")]
    public async Task<ActionResult<AssignmentResponseDto>> GetById(Guid Id)
    {
        try
        {
            var query = new AssignmentGetByIdQuery(Id);
            var result = await mediator.Send(query);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<AssignmentResponseDto>> Create([FromBody] AssignmentRequestDto userRequest)
    {
        try
        {
            var query = new AssignmentsCreateCommand(userRequest);
            var result = await mediator.Send(query);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }
    [HttpPut("{Id:guid}")]
    public async Task<ActionResult<AssignmentResponseDto>> Update([FromRoute] Guid Id, [FromBody] AssignmentRequestToUpdateDto userRequest)
    {
        try
        {
            var query = new AssignmentUpdateCommand(Id, userRequest);
            var result = await mediator.Send(query);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }
    [HttpDelete("{Id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid Id) 
    {
        try
        {
            var query = new AssignmentDeleteCommand(Id);
            var result = await mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }
}
