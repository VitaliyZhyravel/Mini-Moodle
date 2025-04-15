using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.Courses.Commands
{
    public class CourseCreateCommandHandler : IRequestHandler<CourseCreateCommand, OperationResult<CourseResponseCreateUpdateDto>>
    {
        public Moodle_DbContext dbContext { get; }
        public IMapper mapper { get; }

        public CourseCreateCommandHandler(Moodle_DbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<OperationResult<CourseResponseCreateUpdateDto>> Handle(CourseCreateCommand request, CancellationToken cancellationToken)
        {
            var course = mapper.Map<Course>(request.request);

            var isCourseExists = await dbContext.Courses.AnyAsync(c => c.Title == course.Title, cancellationToken);

            if (isCourseExists)
            {
                return OperationResult<CourseResponseCreateUpdateDto>.Failure("Course already exists");
            }

            await  dbContext.Courses.AddAsync(course, cancellationToken);  
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<CourseResponseCreateUpdateDto>.Success(mapper.Map<CourseResponseCreateUpdateDto>(course));
        }
    }
}
