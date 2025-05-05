using AutoMapper;
using MediatR;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Repositories.CourseService.Interfaces;

namespace Mini_Moodle.Repositories.CourseService.Commands.UpdateCourse
{
    public class CourseUpdateCommandHandler : IRequestHandler<CourseUpdateCommand, OperationResult<CourseResponseCreateUpdateDto>>
    {
        private readonly ICourseCommandRepository courseCommand;
        private readonly IMapper mapper;

        public CourseUpdateCommandHandler(ICourseCommandRepository courseCommand,IMapper mapper )
        {
            this.courseCommand = courseCommand;
            this.mapper = mapper;
        } 

        public async Task<OperationResult<CourseResponseCreateUpdateDto>> Handle(CourseUpdateCommand request, CancellationToken cancellationToken)
        {
            var commandResult = await courseCommand.UpdateAsync(request.id,mapper.Map<Course>(request.request), cancellationToken);

            return commandResult.IsSuccess ?
                OperationResult<CourseResponseCreateUpdateDto>.Success(mapper.Map<CourseResponseCreateUpdateDto>(commandResult.Data)) :
                OperationResult<CourseResponseCreateUpdateDto>.Failure(commandResult.ErrorMessage!);
        }
    }
}
