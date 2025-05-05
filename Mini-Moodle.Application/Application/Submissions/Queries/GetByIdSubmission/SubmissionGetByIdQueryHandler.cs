using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using Mini_Moodle.Repositories.SubmissonService.Interfaces;
using System.Net.Http;

namespace Mini_Moodle.Repositories.SubmissionService.Queries.GetByIdSubmission
{
    public class SubmissionGetByIdQueryHandler : IRequestHandler<SubmissionGetByIdQuery, OperationResult<SubmissionResponseDto>>
    {
        private readonly ISubmissionQueryRepository submissionQuery;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<SubmissionGetByIdQueryHandler> logger;
        private readonly IHttpContextAccessor httpContext;

        public SubmissionGetByIdQueryHandler(ISubmissionQueryRepository submissionQuery, IMapper mapper,
            IMemoryCache memoryCache, ILogger<SubmissionGetByIdQueryHandler> logger, IHttpContextAccessor httpContext)
        {
            this.submissionQuery = submissionQuery;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.logger = logger;
            this.httpContext = httpContext;
        }

        public async Task<OperationResult<SubmissionResponseDto>> Handle(SubmissionGetByIdQuery request, CancellationToken cancellationToken)
        {
            var scheme = httpContext!.HttpContext!.Request.Scheme;
            var host = httpContext.HttpContext.Request.Host;

            memoryCache.TryGetValue(request.id, out Submission? cachedSubmission);

            if (cachedSubmission != null)
            {
                logger.LogInformation("Submission retrived from cashe");
                return OperationResult<SubmissionResponseDto>.Success(mapper.Map<SubmissionResponseDto>(cachedSubmission));
            }
            else
            {
                var queryResult = await submissionQuery.GetByIdAsync(request.id, cancellationToken);

                if (queryResult.IsSuccess)
                {
                    logger.LogInformation("Submission retrived from database");
                    memoryCache.Set(request.id, queryResult.Data, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(3600),
                        SlidingExpiration = TimeSpan.FromSeconds(45),
                        Priority = CacheItemPriority.Normal,
                    });
                }
                if (queryResult.IsSuccess)
                {
                    if (queryResult!.Data!.ProjectPath != null)
                      queryResult.Data.ProjectPath = $"{scheme}://{host}{queryResult.Data.ProjectPath}";

                    return  OperationResult<SubmissionResponseDto>.Success(mapper.Map<SubmissionResponseDto>(queryResult.Data));
                }

               return OperationResult<SubmissionResponseDto>.Failure(queryResult.ErrorMessage!);

            }

        }
    }
}
