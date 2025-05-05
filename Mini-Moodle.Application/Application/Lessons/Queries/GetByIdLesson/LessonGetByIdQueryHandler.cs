using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Repositories.LessonService.Interfaces;
using System.Net.Http;

namespace Mini_Moodle.Repositories.LessonService.Queries.GetByIdLesson;

public class LessonGetByIdQueryHandler : IRequestHandler<LessonGetByIdQuery, OperationResult<LessonResponseDto>>
{
    private readonly ILessonQueryRepository lessonQuery;

    private readonly IMapper mapper;
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<LessonGetByIdQueryHandler> logger;
    private readonly IHttpContextAccessor httpContext;

    public LessonGetByIdQueryHandler(ILessonQueryRepository lessonQuery, IMapper mapper,
        IMemoryCache memoryCache, ILogger<LessonGetByIdQueryHandler> logger, IHttpContextAccessor httpContext)
    {
        this.lessonQuery = lessonQuery;
        this.mapper = mapper;
        this.memoryCache = memoryCache;
        this.logger = logger;
        this.httpContext = httpContext;
    }

    public async Task<OperationResult<LessonResponseDto>> Handle(LessonGetByIdQuery request, CancellationToken cancellationToken)
    {
        var host = httpContext!.HttpContext!.Request.Host;
        var scheme = httpContext.HttpContext.Request.Scheme;

        memoryCache.TryGetValue(request.id, out Lesson? cashedLesson);

        if (cashedLesson != null)
        {
            logger.LogInformation("Lesson retrived from cache");
            return OperationResult<LessonResponseDto>.Success(mapper.Map<LessonResponseDto>(cashedLesson));
        }
        else
        {
            var queryResult = await lessonQuery.GetByIdAsync(request.id, cancellationToken);
            if (queryResult.IsSuccess)
            {
                logger.LogInformation("Lesson retrived from database");
                memoryCache.Set(request.id, queryResult.Data, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(3600),
                    SlidingExpiration = TimeSpan.FromSeconds(45),
                    Priority = CacheItemPriority.Normal,
                });
            }

            if (queryResult.IsSuccess)
            {
                if (queryResult!.Data!.VideoUrl != null)
                    queryResult.Data.VideoUrl = $"{scheme}://{host}{queryResult.Data.VideoUrl}";

                return OperationResult<LessonResponseDto>.Success(mapper.Map<LessonResponseDto>(queryResult.Data));
            }

            return OperationResult<LessonResponseDto>.Failure(queryResult.ErrorMessage!);
        }

    }
}
