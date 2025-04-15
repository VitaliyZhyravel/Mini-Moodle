using AutoMapper;
using MediatR;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.Courses.Commands
{
    public class CourseDeleteCommandHandler : IRequestHandler<CourseDeleteCommand, OperationResult<CourseResponseDto>>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly IMapper mapper;

        public CourseDeleteCommandHandler(Moodle_DbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<OperationResult<CourseResponseDto>> Handle(CourseDeleteCommand request, CancellationToken cancellationToken)
        {
           var course = await dbContext.Courses.FindAsync(request.id,cancellationToken);

            if (course == null)
            {
                return OperationResult<CourseResponseDto>.Failure("Course not found");
            }
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<CourseResponseDto>.Success(mapper.Map<CourseResponseDto>(course));
        }
    }

}
