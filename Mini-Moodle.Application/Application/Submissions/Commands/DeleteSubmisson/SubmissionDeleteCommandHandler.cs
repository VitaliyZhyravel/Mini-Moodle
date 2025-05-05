using AutoMapper;
using MediatR;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using Mini_Moodle.Repositories.SubmissonService.Interfaces;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.DeleteSubmisson
{
    public class SubmissionDeleteCommandHandler : IRequestHandler<SubmissionDeleteCommand, OperationResult<Guid>>
    {
        private readonly ISubmissionCommandRepository submissionCommand;

        public SubmissionDeleteCommandHandler(ISubmissionCommandRepository submissionCommand)
        {
            this.submissionCommand = submissionCommand;
        }

        public async Task<OperationResult<Guid>> Handle(SubmissionDeleteCommand request, CancellationToken cancellationToken)
        {
            var commandResult = await submissionCommand.DeleteAsync(request.id, cancellationToken);

            return commandResult.IsSuccess ? OperationResult<Guid>.Success(commandResult.Data)
                : OperationResult<Guid>.Failure(commandResult.ErrorMessage!);
        }
    }
}
