using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Repositories.CourseService.Interfaces;

namespace Mini_Moodle.Repositories.CourseService.Implementation
{
    public class CourseCommandRepository : ICourseCommandRepository
    {
        private readonly Moodle_DbContext dbContext;

        public CourseCommandRepository(Moodle_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OperationResult<Course>> CreateAsync(Course userRequest, CancellationToken cancellationToken)
        {
            var isCourseExists = await dbContext.Courses.AnyAsync(c => c.Title == userRequest.Title, cancellationToken);

            if (isCourseExists)
            {
                return OperationResult<Course>.Failure("Course already exists");
            }

            await dbContext.Courses.AddAsync(userRequest, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<Course>.Success(userRequest);
        }

        public async Task<OperationResult<Guid>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var course = await dbContext.Courses.FindAsync(id, cancellationToken);

            if (course == null)
            {
                return OperationResult<Guid>.Failure("Course not found");
            }
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<Guid>.Success(course.Id);
        }

        public async Task<OperationResult<Course>> UpdateAsync(Guid id, Course userRequest, CancellationToken cancellationToken)
        {
            var course = dbContext.Courses
                .FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return OperationResult<Course>.Failure("Course not found");
            }
            
            course.Title = userRequest.Title;
            course.Description = userRequest.Description;
            course.CreatedAt = userRequest.CreatedAt;
            course.CreatedBy = userRequest.CreatedBy;

            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<Course>.Success(course);
        }
    }
}
