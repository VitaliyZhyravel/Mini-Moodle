using AutoMapper;
using MediatR;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.Courses.Commands
{
    public class CourseUpdateCommandHandler : IRequestHandler<CourseUpdateCommand, OperationResult<CourseResponseCreateUpdateDto>>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly IMapper mapper;

        public CourseUpdateCommandHandler(Moodle_DbContext dbContext,IMapper mapper )
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<OperationResult<CourseResponseCreateUpdateDto>> Handle(CourseUpdateCommand request, CancellationToken cancellationToken)
        {
           var course = dbContext.Courses
                .FirstOrDefault(c => c.Id == request.id);
            if (course == null)
            {
                return OperationResult<CourseResponseCreateUpdateDto>.Failure("Course not found");
            }
            var updatedCourse =  mapper.Map<Course>(request.request);

            updatedCourse.Id = course.Id; 
            course.Title = updatedCourse.Title;
            course.Description = updatedCourse.Description;
            course.CreatedAt = updatedCourse.CreatedAt;
            course.CreatedBy = updatedCourse.CreatedBy;

            await dbContext.SaveChangesAsync(cancellationToken);
            return OperationResult<CourseResponseCreateUpdateDto>.Success(mapper.Map<CourseResponseCreateUpdateDto>(updatedCourse));
        }
    }
}
