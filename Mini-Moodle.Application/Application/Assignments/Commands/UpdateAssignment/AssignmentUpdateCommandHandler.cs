using AutoMapper;
using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.AssignmentDto;
using Mini_Moodle.Repositories.AssignmentServices.Interfaces;

namespace Mini_Moodle.Repositories.AssignmentsService.Commands.UpdateAssignment;

public class AssignmentUpdateCommandHandler : IRequestHandler<AssignmentUpdateCommand, OperationResult<AssignmentResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IAssignmentCommandRepository assignmentCommand;

    public AssignmentUpdateCommandHandler(IMapper mapper,IAssignmentCommandRepository assignmentCommand)
    {
        this.mapper = mapper;
        this.assignmentCommand = assignmentCommand;
    }

    public async  Task<OperationResult<AssignmentResponseDto>> Handle(AssignmentUpdateCommand request, CancellationToken cancellationToken)
    {
      var result = await assignmentCommand.UpdateAsync(request.id, mapper.Map<Assignment>(request.userRequest),
            request.userRequest.AddMinutesToDeadLine, cancellationToken);

      return  result.IsSuccess ?
            OperationResult<AssignmentResponseDto>.Success(mapper.Map<AssignmentResponseDto>(result.Data)) :
            OperationResult<AssignmentResponseDto>.Failure(result.ErrorMessage);
    }
}
