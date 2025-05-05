using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.SubmissionDtos;

namespace Mini_Moodle.Repositories.SubmissionService.Queries.GetByIdSubmission;

public record SubmissionGetByIdQuery(Guid id) : IRequest<OperationResult<SubmissionResponseDto>> { }
