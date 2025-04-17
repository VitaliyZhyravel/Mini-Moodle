using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.Courses.Queries
{
    public class CourseGetAllQueryHandler : IRequestHandler<CourseGetAllQuery, OperationResult<List<CourseResponseDto>>>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly IMapper mapper;

        public CourseGetAllQueryHandler(Moodle_DbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<OperationResult<List<CourseResponseDto>>> Handle(CourseGetAllQuery request, CancellationToken cancellationToken)
        {
            List<Course>? allCourses = await dbContext.Courses
                .AsNoTracking()
                .Include(c => c.Lessons)
                .ToListAsync(cancellationToken);

            //Filtering
            if (!string.IsNullOrWhiteSpace(request.filterBy) && !string.IsNullOrWhiteSpace(request.filterOn))
            {
                switch (request.filterBy)
                {
                    case nameof(Course.Title):
                        allCourses = allCourses.Where(c => c.Title.Contains(request.filterOn)).ToList();
                        break;

                    case nameof(Course.Description):
                        allCourses = allCourses.Where(c => c.Description?.Contains(request.filterOn) ?? false).ToList();
                        break;
                }
            }
            //Sorting
            if (!string.IsNullOrWhiteSpace(request.sortBy))
            {
                switch (request.sortBy)
                {
                    case nameof(Course.Title):
                        allCourses = request.isAscending ? allCourses.OrderBy(c => c.Title).ToList() : allCourses.OrderByDescending(c => c.Title).ToList();
                        break;

                    case nameof(Course.Description):
                        allCourses = request.isAscending ? allCourses.OrderBy(c => c.Description).ToList() : allCourses.OrderByDescending(c => c.Description).ToList();
                        break;
                }
            }
            //Paging
            allCourses = allCourses.Skip((request.pageNumber - 1) * request.pageSize).Take(request.pageSize).ToList();

          var courseDto =  mapper.Map<List<CourseResponseDto>>(allCourses);

            return OperationResult<List<CourseResponseDto>>.Success(courseDto);
        }
    }
}
