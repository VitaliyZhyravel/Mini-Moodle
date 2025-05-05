using MediatR;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.GradeDto;

namespace Mini_Moodle.Repositories.GradeService.CreateGrade;

public record GradeCreateCommand(GradeRequestDto userRequest) : IRequest<OperationResult<GradeResponseDto>> { }

