using AutoMapper;
using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using Mini_Moodle.Repositories.SubmissonService.Interfaces;

namespace Mini_Moodle.Repositories.SubmissionService.Commands.CreateSubmisson;

public class SubmissionCreateCommandHandler : IRequestHandler<SubmissionCreateCommand, OperationResult<SubmissionResponseDto>>
{
    private readonly IMapper mapper;
    private readonly ISubmissionCommandRepository submissionCommand;

    public SubmissionCreateCommandHandler(IMapper mapper, ISubmissionCommandRepository submissionCommand)
    {
        this.mapper = mapper;
        this.submissionCommand = submissionCommand;
    }

    public async Task<OperationResult<SubmissionResponseDto>> Handle(SubmissionCreateCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Submission>(request.userRequest);

        var commandResult = await submissionCommand.CreateAsync(entity, cancellationToken);

        return commandResult.IsSuccess ? OperationResult<SubmissionResponseDto>.Success(mapper.Map<SubmissionResponseDto>(commandResult.Data))
            : OperationResult<SubmissionResponseDto>.Failure(commandResult.ErrorMessage!); 
    }
}
