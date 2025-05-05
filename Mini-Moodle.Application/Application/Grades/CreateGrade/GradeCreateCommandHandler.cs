using AutoMapper;
using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.GradeDto;
using Mini_Moodle.Repositories.GradeService.Interfaces;

namespace Mini_Moodle.Repositories.GradeService.CreateGrade;

public class GradeCreateCommandHandler : IRequestHandler<GradeCreateCommand, OperationResult<GradeResponseDto>>
{
    private readonly IGradeCommandRepository gradeCommand;
    private readonly IMapper mapper;

    public GradeCreateCommandHandler(IGradeCommandRepository gradeCommand, IMapper mapper)
    {
        this.gradeCommand = gradeCommand;
        this.mapper = mapper;
    }

    public async Task<OperationResult<GradeResponseDto>> Handle(GradeCreateCommand request, CancellationToken cancellationToken)
    {
      var commandResult = await gradeCommand.CreateAsync(mapper.Map<Grade>(request.userRequest), cancellationToken);
        return commandResult.IsSuccess ?
            OperationResult<GradeResponseDto>.Success(mapper.Map<GradeResponseDto>(commandResult.Data)) :
            OperationResult<GradeResponseDto>.Failure(commandResult.ErrorMessage!);
    }
}
