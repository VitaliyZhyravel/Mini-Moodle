using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Repositories.CourseService.Interfaces;

namespace Mini_Moodle.Repositories.CourseService.Queries.GetAllCourses
{
    public class CourseGetAllQueryHandler : IRequestHandler<CourseGetAllQuery, OperationResult<List<CourseResponseDto>>>
    {
        private readonly ICourseQueryRepository courseQuery;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<CourseGetAllQueryHandler> logger;

        public CourseGetAllQueryHandler(ICourseQueryRepository courseQuery, IMapper mapper, IMemoryCache memoryCache,ILogger<CourseGetAllQueryHandler> logger)
        {
            this.courseQuery = courseQuery;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        private record CourseCacheKey(string? filterBy, string? filterOn, string? sortBy, bool isAscending, int pageNumber, int pageSize) { }


        public async Task<OperationResult<List<CourseResponseDto>>> Handle(CourseGetAllQuery request, CancellationToken cancellationToken)
         {
            memoryCache.TryGetValue(new CourseCacheKey
                (
                    request.filterBy,
                    request.filterOn,
                    request.sortBy,
                    request.isAscending,
                    request.pageNumber,
                    request.pageSize
                ), out List<Course>? cachedCourse);

            if (cachedCourse != null)
            {
                logger.LogInformation($"Courses retrieved from cache");

                return OperationResult<List<CourseResponseDto>>.Success(mapper.Map<List<CourseResponseDto>>(cachedCourse));
            }
            else
            {
                var queryResult = await courseQuery.GetAllAsync(request.filterBy, request.filterOn, request.sortBy, request.isAscending, request.pageNumber, request.pageSize, cancellationToken);

                logger.LogInformation($"Courses retrieved database");

                memoryCache.Set(new CourseCacheKey
                    (
                        request.filterBy,
                        request.filterOn,
                        request.sortBy,
                        request.isAscending,
                        request.pageNumber,
                        request.pageSize
                    ), queryResult.Data,new MemoryCacheEntryOptions 
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(3600),
                        SlidingExpiration = TimeSpan.FromSeconds(45),
                        Priority = CacheItemPriority.Normal,
                    });

                return queryResult.IsSuccess ?
                        OperationResult<List<CourseResponseDto>>.Success(mapper.Map<List<CourseResponseDto>>(queryResult.Data)) :
                        OperationResult<List<CourseResponseDto>>.Failure(queryResult.ErrorMessage!);
            }
        }
    }
}
