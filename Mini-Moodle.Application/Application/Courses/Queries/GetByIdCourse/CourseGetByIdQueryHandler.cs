using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Repositories.CourseService.Interfaces;

namespace Mini_Moodle.Repositories.CourseService.Queries.GetByIdCourse
{
    public class CourseGetByIdQueryHandler : IRequestHandler<CourseGetByIdQuery,OperationResult<CourseResponseDto>>
    {
        private readonly ICourseQueryRepository courseQuery;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<CourseGetByIdQueryHandler> logger;

        public CourseGetByIdQueryHandler(ICourseQueryRepository courseQuery, IMapper mapper,IMemoryCache memoryCache,ILogger<CourseGetByIdQueryHandler> logger)
        {
            this.courseQuery = courseQuery;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        
        

        public async Task<OperationResult<CourseResponseDto>> Handle(CourseGetByIdQuery request, CancellationToken cancellationToken)
        {
            memoryCache.TryGetValue(request.id, out Course? cachedCourse);

            if (cachedCourse != null)
            {
                logger.LogInformation($"Course retrieved from cache");
                return OperationResult<CourseResponseDto>.Success(mapper.Map<CourseResponseDto>(cachedCourse));
            }
            else
            {
                var queryResult = await courseQuery.GetByIdAsync(request.id, cancellationToken);

                if (queryResult.IsSuccess)
                {
                    logger.LogInformation($"Course retrieved from database");
                    memoryCache.Set(request.id, queryResult.Data, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(3600),
                        SlidingExpiration = TimeSpan.FromSeconds(45),
                        Priority = CacheItemPriority.Normal,
                    });
                    
                }
               
                return queryResult.IsSuccess ?
                    OperationResult<CourseResponseDto>.Success(mapper.Map<CourseResponseDto>(queryResult.Data)) :
                    OperationResult<CourseResponseDto>.Failure(queryResult.ErrorMessage!);
            }
               
        }
    }
}
