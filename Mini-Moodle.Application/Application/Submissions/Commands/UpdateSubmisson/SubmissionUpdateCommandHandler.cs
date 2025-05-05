using AutoMapper;
using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using Mini_Moodle.Repositories.SubmissonService.Interfaces;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.UpdateSubmisson;

public class SubmissionUpdateCommandHandler : IRequestHandler<SubmissionUpdateCommand, OperationResult<SubmissionResponseDto>>
{
    private readonly ISubmissionCommandRepository submissionCommand;
    private readonly IMapper mapper;

    public SubmissionUpdateCommandHandler(ISubmissionCommandRepository submissionCommand,IMapper mapper)
    {
        this.submissionCommand = submissionCommand;
        this.mapper = mapper;
    }

    public async Task<OperationResult<SubmissionResponseDto>> Handle(SubmissionUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Submission>(request.userRequest);

        var commandResult = await submissionCommand.UpdateAsync(request.id, entity, cancellationToken);

        return commandResult.IsSuccess ? OperationResult<SubmissionResponseDto>.Success(mapper.Map<SubmissionResponseDto>(commandResult.Data))
            : OperationResult<SubmissionResponseDto>.Failure(commandResult.ErrorMessage!);
    }
}
