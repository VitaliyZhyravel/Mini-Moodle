using AutoMapper;
using MediatR;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Repositories.CourseService.Interfaces;
using Mini_Moodle.Repositories.LessonService.Interfaces;

namespace Mini_Moodle.Repositories.CourseService.Commands.DeleteCourse
{
    public class CourseDeleteCommandHandler : IRequestHandler<CourseDeleteCommand, OperationResult<Guid>>
    {
        private readonly ICourseCommandRepository courseCommand;

        public CourseDeleteCommandHandler(ICourseCommandRepository courseCommand)
        {
            this.courseCommand = courseCommand;
        }

        public async Task<OperationResult<Guid>> Handle(CourseDeleteCommand request, CancellationToken cancellationToken)
        {
            var commandResult = await courseCommand.DeleteAsync(request.id, cancellationToken);

            return commandResult.IsSuccess ? OperationResult<Guid>.Success(commandResult.Data )
                  : OperationResult<Guid>.Failure(commandResult.ErrorMessage!);
        }
    }
}


