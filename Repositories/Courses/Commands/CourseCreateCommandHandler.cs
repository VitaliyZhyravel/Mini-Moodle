using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto;

namespace Mini_Moodle.Repositories.Courses.Commands
{
    public class CourseCreateCommandHandler : IRequestHandler<CourseCreateCommand, OperationResult<CourseResponseUpdateDto>>
    {
        public Moodle_DbContext dbContext { get; }
        public IMapper mapper { get; }

        public CourseCreateCommandHandler(Moodle_DbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<OperationResult<CourseResponseUpdateDto>> Handle(CourseCreateCommand request, CancellationToken cancellationToken)
        {
            var course = mapper.Map<Course>(request.request);

            var isCourseExists = await dbContext.Courses.AnyAsync(c => c.Title == course.Title, cancellationToken);

            if (isCourseExists)
            {
                return OperationResult<CourseResponseUpdateDto>.Failure("Course already exists");
            }

            await  dbContext.Courses.AddAsync(course, cancellationToken);  
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<CourseResponseUpdateDto>.Success(mapper.Map<CourseResponseUpdateDto>(course));
        }
    }
}
