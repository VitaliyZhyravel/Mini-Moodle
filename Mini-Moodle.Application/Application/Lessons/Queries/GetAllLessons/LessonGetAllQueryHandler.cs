using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Repositories.LessonService.Interfaces;

namespace Mini_Moodle.Repositories.LessonService.Queries.GetAllLessons;

public class LessonGetAllQueryHandler : IRequestHandler<LessonGetAllQuery, OperationResult<List<LessonResponseDto>>>
{
    private readonly ILessonQueryRepository lessonQuery;
    private readonly IMapper mapper;
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<LessonGetAllQueryHandler> logger;
    private readonly IHttpContextAccessor httpContext;

    public LessonGetAllQueryHandler(ILessonQueryRepository lessonQuery, IMapper mapper,
        IMemoryCache memoryCache, ILogger<LessonGetAllQueryHandler> logger, IHttpContextAccessor httpContext)
    {
        this.lessonQuery = lessonQuery;
        this.mapper = mapper;
        this.memoryCache = memoryCache;
        this.logger = logger;
        this.httpContext = httpContext;
    }

    private record LessonCacheKey(string? filterBy, string? filterOn, string? sortBy, bool isAscending, int pageNumber, int pageSize) { }

    public async Task<OperationResult<List<LessonResponseDto>>> Handle(LessonGetAllQuery request, CancellationToken cancellationToken)
    {
        var host = httpContext.HttpContext?.Request.Host;
        var scheme = httpContext.HttpContext?.Request.Scheme;

        memoryCache.TryGetValue(new LessonCacheKey
            (
                    request.filterBy,
                    request.filterOn,
                    request.sortBy,
                    request.isAscending,
                    request.pageNumber,
                    request.pageSize
            ), out List<Lesson>? cashedLesson);

        if (cashedLesson != null)
        {
            logger.LogInformation($"Lessons retrieved from cache");
            return OperationResult<List<LessonResponseDto>>.Success(mapper.Map<List<LessonResponseDto>>(cashedLesson));
        }
        else
        {
            var queryResult = await lessonQuery.GetAllAsync(request.filterBy, request.filterOn, request.sortBy, request.isAscending, request.pageNumber, request.pageSize, cancellationToken);

            if (queryResult.IsSuccess)
            {
                logger.LogInformation($"Lessons retrieved from database");

                memoryCache.Set(new LessonCacheKey
                    (
                        request.filterBy,
                        request.filterOn,
                        request.sortBy,
                        request.isAscending,
                        request.pageNumber,
                        request.pageSize
                    ), queryResult.Data, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(3600),
                        SlidingExpiration = TimeSpan.FromSeconds(45),
                        Priority = CacheItemPriority.Normal
                    });
            }

            if (queryResult.IsSuccess)
            {
                var mappedLessons = mapper.Map<List<LessonResponseDto>>(queryResult.Data);

                foreach (var item in mappedLessons)
                {
                    if (item.VideoUrl != null)
                    {
                        item.VideoUrl = $"{scheme}://{host}{item.VideoUrl}";
                    }
                }

              return  OperationResult<List<LessonResponseDto>>.Success(mappedLessons);
            }

            return OperationResult<List<LessonResponseDto>>.Failure(queryResult.ErrorMessage!);
        }


    }
}
