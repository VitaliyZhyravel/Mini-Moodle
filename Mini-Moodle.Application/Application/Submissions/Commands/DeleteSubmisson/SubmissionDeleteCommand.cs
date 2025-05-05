using MediatR;
using Mini_Moodle.Exceptions;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.DeleteSubmisson;

public record SubmissionDeleteCommand(Guid id) : IRequest<OperationResult<Guid>> { }

