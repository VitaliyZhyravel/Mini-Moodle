using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Filters;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Repositories.CourseService.Commands.CreateCourse;
using Mini_Moodle.Repositories.CourseService.Commands.DeleteCourse;
using Mini_Moodle.Repositories.CourseService.Commands.UpdateCourse;
using Mini_Moodle.Repositories.CourseService.Queries.GetAllCourses;
using Mini_Moodle.Repositories.CourseService.Queries.GetByIdCourse;

namespace Mini_Moodle.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class CourseController : ControllerBase
{
    private readonly IMediator mediator;

    public CourseController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Student,Teacher")]
    [TypeFilter(typeof(ActionFilterValidateQueryParameters))]
    [HttpGet]
    public async Task<ActionResult<List<CourseResponseDto>>> GetAll([FromQuery] string? filterBy = null, [FromQuery] string? filterOn = null,
        [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true, int pageNumber = 0, int pageSize = 1000)
    {
        try
        {
            var query = new CourseGetAllQuery(filterBy, filterOn, sortBy, isAscending, pageNumber, pageSize);
            var result = await mediator.Send(query);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }

    [HttpGet("{Id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Student,Teacher")]
    public async Task<ActionResult<CourseResponseDto>> GetById([FromRoute] Guid Id)
    {
        try
        {
            var query = new CourseGetByIdQuery(Id);
            var result = await mediator.Send(query);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CourseResponseDto>> Create([FromBody] CourseRequestToCreateDto userRequest)
    {
        try
        {
            var query = new CourseCreateCommand(userRequest);
            var result = await mediator.Send(query);

            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { Id = result.Data.Id }, result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }

    [HttpPut("{Id:guid}")]
    public async Task<ActionResult<CourseResponseCreateUpdateDto>> Update([FromRoute] Guid Id, [FromBody] CourseRequestToUpdateDto userRequest)
    {
        try
        {
            var query = new CourseUpdateCommand(Id, userRequest);
            var result = await mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }

    [HttpDelete("{Id:guid}")]
    public async Task<ActionResult<CourseResponseDto>> Delete([FromRoute] Guid Id) 
    {
        try
        {
            var query = new CourseDeleteCommand(Id);
            var result = await mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
        }
    }
}