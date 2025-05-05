using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using Mini_Moodle.Repositories.SubmissionService.Commands.CreateSubmisson;
using Mini_Moodle.Repositories.SubmissionService.Commands.UpdateSubmisson;
using Mini_Moodle.Repositories.SubmissionService.Commands.UploadVideoSubmisson;
using Mini_Moodle.Repositories.SubmissionService.Queries.GetByIdSubmission;

namespace Mini_Moodle.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SubmissionController : ControllerBase
    {
        private readonly IMediator mediator;

        public SubmissionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<SubmissionResponseDto>> Create(SubmissionRequestDto userRequest)
        {
            try
            {
                var query = new SubmissionCreateCommand(userRequest);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }

        [HttpPost("{Id:guid}/UploadFile")]
        public async Task<ActionResult<SubmissionResponseDto>> UploadFile(Guid Id, SubmissionFileUploadDto file)
        {
            try
            {
                var query = new SubmissionUploadFilesCommand(Id, file);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }
        [HttpGet("{Id:guid}")]
        public async  Task<ActionResult<SubmissionResponseDto>> GetById([FromRoute] Guid Id)
        {
            try
            {
                var query = new SubmissionGetByIdQuery(Id);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex )
            {
                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id) 
        {
            try
            {
                var query = new SubmissionGetByIdQuery(Id);
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {

                return BadRequest($"Exeption Message : {ex.Message}\nInner exception: {ex.InnerException?.Message}");
            }
        }

        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id, SubmissionRequestToUpdate userRequest)
        {
            try
            {
                var query = new SubmissionUpdateCommand(Id,userRequest);
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
