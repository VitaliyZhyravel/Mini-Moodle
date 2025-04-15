using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Filters;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Repositories.Lesson;

namespace Mini_Moodle.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IMediator mediator;

        public LessonController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<LessonResponseToCreateUpdateDto>> Create([FromForm] LessonRequestToCreateDto userRequest) 
        {
            try
            {
                var query = new LessonCreateCommand(userRequest);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {

                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }
    }
}
