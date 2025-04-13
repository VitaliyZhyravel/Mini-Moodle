using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Filters;
using Mini_Moodle.Models.Dto;
using Mini_Moodle.Repositories.Courses.Commands;
using Mini_Moodle.Repositories.Courses.Queries;

namespace Mini_Moodle.Controllers.v1
{
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Student,Teacher")]
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
                return BadRequest($"An error occurred while retrieving the courses {ex.InnerException?.Message}");
            }
        }
        [HttpGet("{Id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Student,Teacher")]
        public async Task<ActionResult<CourseResponseDto>> GetByid([FromRoute] Guid Id) 
        {
            try
            {
                var query = new CourseGetByIdQuery(Id);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while retrieving the course {ex.InnerException?.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<CourseResponseDto>> Create([FromBody] CourseRequestToCreateDto request) 
        {
            try
            {
                var query = new CourseCreateCommand(request);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while retrieving the course {ex.InnerException?.Message}");
            }
        }
    }
}