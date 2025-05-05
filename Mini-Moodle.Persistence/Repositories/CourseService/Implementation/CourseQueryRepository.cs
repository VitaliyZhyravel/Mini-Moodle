using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Repositories.CourseService.Interfaces;

namespace Mini_Moodle.Repositories.CourseService.Implementation
{
    public class CourseQueryRepository : ICourseQueryRepository
    {
        private readonly Moodle_DbContext dbContext;

        public CourseQueryRepository(Moodle_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OperationResult<List<Course?>>> GetAllAsync(string? filterBy, string? filterOn, string? sortBy, bool isAscending, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var allLessons = dbContext.Courses
                .Include(Courses => Courses.Lessons)
                .ThenInclude(Lessons => Lessons.Assignments)
                .AsNoTracking();

            const string byTitle = "TITLE";
            const string byDescription = "DESCRIPTION";

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterBy) && !string.IsNullOrWhiteSpace(filterOn))
            {
                switch (filterBy.ToUpper())
                {
                    case byTitle:
                        allLessons = allLessons.Where(c => c.Title.ToLower().Contains(filterOn.ToLower()));
                        break;
                    case byDescription:
                        allLessons = allLessons.Where(c => c.Description.ToLower().Contains(filterOn.ToLower()));
                        break;
                    default:
                        return OperationResult<List<Course?>>.Failure("Invalid filter by parameter");
                }
            }
            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToUpper())
                {
                    case byTitle:
                        allLessons = isAscending ? allLessons.OrderBy(c => c.Title) : allLessons.OrderByDescending(c => c.Title);
                        break;

                    case byDescription:
                        allLessons = isAscending ? allLessons.OrderBy(c => c.Description) : allLessons.OrderByDescending(c => c.Description);
                        break;
                    default:
                        return OperationResult<List<Course?>>.Failure("Invalid sort by parameter");
                }
            }

            if (pageSize > 0 && pageNumber > 0)
            {
                //Paging
                allLessons = allLessons.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

           return OperationResult<List<Course?>>.Success(await allLessons.ToListAsync(cancellationToken));
        }

        public async Task<OperationResult<Course?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await dbContext.Courses.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (result == null)
            {
                return OperationResult<Course?>.Failure("Course not found");
            }
            return OperationResult<Course?>.Success(result);
        }
    }
}
