using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Repositories.LessonService.Commands.CreateLesson;
using Mini_Moodle.Repositories.LessonService.Commands.DeleteLesson;
using Mini_Moodle.Repositories.LessonService.Commands.UpdateLesson;
using Mini_Moodle.Repositories.LessonService.Commands.UploadLessonVideo;
using Mini_Moodle.Repositories.LessonService.Queries.GetAllLessons;
using Mini_Moodle.Repositories.LessonService.Queries.GetByIdLesson;

namespace Mini_Moodle.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LessonController : ControllerBase
    {
        private readonly IMediator mediator;

        public LessonController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<LessonResponseDto>>> GetAll([FromQuery] string? filterBy = null, [FromQuery] string? filterOn = null,
            [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true, int pageNumber = 0, int pageSize = 1000)
        {
            try
            {
                var query = new LessonGetAllQuery(filterBy, filterOn, sortBy, isAscending, pageNumber, pageSize);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {

                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<LessonResponseDto>> GetById([FromRoute] Guid Id)
        {
            try
            {
                var query = new LessonGetByIdQuery(Id);
                var result = await mediator.Send(query);
                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LessonResponseToCreateUpdateDto>> Create([FromBody] LessonRequestToCreateDto userRequest)
        {
            try
            {
                var query = new LessonCreateCommand(userRequest);
                var result = await mediator.Send(query);

                return result.IsSuccess ? CreatedAtAction(nameof(GetById),new {Id = result.Data?.Id },result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }
        [HttpPost("{Id:guid}/UploadVideo")]
        public async Task<IActionResult> UploadVideo([FromRoute] Guid Id, [FromForm] UploadVideoDto uploadVideo)
        {
            try
            {
                var query = new UploadVideoCommand(Id, uploadVideo.file);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<LessonResponseToCreateUpdateDto>> Update([FromRoute] Guid Id, [FromBody] LessonRequestToCreateDto userRequest)
        {
            try
            {
                var query = new LessonUpdateCommand(Id, userRequest);
                var result = await mediator.Send(query);
                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<LessonResponseToCreateUpdateDto>> Delete(Guid Id)
        {
            try
            {
                var query = new LessonDeleteCommand(Id);
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
