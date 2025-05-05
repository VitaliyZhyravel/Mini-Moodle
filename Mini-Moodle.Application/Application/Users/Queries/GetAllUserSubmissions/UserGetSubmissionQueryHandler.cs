using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using Mini_Moodle.Repositories.UserService.Interfaces;
using System.Security.Claims;

namespace Mini_Moodle.Repositories.UserService.Queries.GetAllUserSubmissions;

public class UserGetSubmissionQueryHandler : IRequestHandler<UserGetSubmissionQuery, OperationResult<List<SubmissionResponseForUserDto>>>
{
    private readonly IUserQueryRepository userCommand;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContext;
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<UserGetSubmissionQueryHandler> logger;

    public UserGetSubmissionQueryHandler(IUserQueryRepository userCommand, IMapper mapper, IHttpContextAccessor httpContext, IMemoryCache memoryCache, ILogger<UserGetSubmissionQueryHandler> logger)
    {
        this.userCommand = userCommand;
        this.mapper = mapper;
        this.httpContext = httpContext;
        this.memoryCache = memoryCache;
        this.logger = logger;
    }

    public async Task<OperationResult<List<SubmissionResponseForUserDto>>> Handle(UserGetSubmissionQuery request, CancellationToken cancellationToken)
    {
        var userIdStr = httpContext?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdStr != null)
        {
            memoryCache.TryGetValue(userIdStr, out List<Submission>? cachedSubmissions);

            if (cachedSubmissions != null)
            {
                logger.LogInformation("Submissions retrieved from cache");
                return OperationResult<List<SubmissionResponseForUserDto>>.Success(mapper.Map<List<SubmissionResponseForUserDto>>(cachedSubmissions));
            }
        }
        var queryResult = await userCommand.GetSubmissionsAsync(cancellationToken);

        if (queryResult.IsSuccess)
        {
            logger.LogInformation("Submissions retrieved from database");
            memoryCache.Set(userIdStr!, queryResult.Data, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(45),
                Priority = CacheItemPriority.Normal,
            });
        }

        return queryResult.IsSuccess ? OperationResult<List<SubmissionResponseForUserDto>>.Success(mapper.Map<List<SubmissionResponseForUserDto>>(queryResult.Data)) :
         OperationResult<List<SubmissionResponseForUserDto>>.Failure(queryResult.ErrorMessage!);
    }
}
//Можна покращити код, тим що збергіти в кеші не Submission, а SubmissionResponseForUserDto