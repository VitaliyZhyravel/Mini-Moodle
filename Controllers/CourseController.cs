using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Filters;
        using Mini_Moodle.Models.Dto;
using Mini_Moodle.Repositories.Courses.Queries;

namespace Mini_Moodle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator mediator;

        public CourseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [TypeFilter(typeof(ActionFilterValidateQueryParameters))]
        [HttpGet]
        public async Task<ActionResult<List<CourseResponseDto>>> GetAll([FromQuery] string? filterBy = null, [FromQuery] string? filterOn = null,
            [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true, int pageNumber = 0, int pageSize = 1000)
        {
            try
            {
                var query = new CourseGetAllQuery(filterBy, filterOn, sortBy, isAscending, pageNumber, pageSize);
                var result = await mediator.Send(query);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the courses.", ex.InnerException);
            }
        }
    }
}