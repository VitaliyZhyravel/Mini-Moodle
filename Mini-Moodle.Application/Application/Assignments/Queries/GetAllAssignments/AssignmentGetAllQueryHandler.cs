using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.AssignmentDto;
using Mini_Moodle.Repositories.AssignmentServices.Interfaces;

namespace Mini_Moodle.Repositories.AssignmentsService.Queries.GetAllAssignment;

public class AssignmentGetAllQueryHandler : IRequestHandler<AssignmentGetAllQuery, OperationResult<List<AssignmentResponseDto>>>
{
    private readonly IMapper mapper;
    private readonly IAssignmentQueryRepository assignmentQuery;
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<AssignmentGetAllQueryHandler> logger;

    public AssignmentGetAllQueryHandler(IMapper mapper, IAssignmentQueryRepository assignmentQuery, IMemoryCache memoryCache, ILogger<AssignmentGetAllQueryHandler> logger)
    {
        this.mapper = mapper;
        this.assignmentQuery = assignmentQuery;
        this.memoryCache = memoryCache;
        this.logger = logger;
    }

    private record AssignmentCacheKey(string? filterBy, string? filterOn, string? sortBy, bool isAscending, int pageNumber, int pageSize) { }


    public async Task<OperationResult<List<AssignmentResponseDto>>> Handle(AssignmentGetAllQuery request, CancellationToken cancellationToken)
    {

        memoryCache.TryGetValue(new AssignmentCacheKey
            (
                request.filterBy,
                request.filterOn,
                request.sortBy,
                request.isAscending,
                request.pageNumber,
                request.pageSize
            ), out List<Assignment>? cachedAssignment);

        if (cachedAssignment != null)
        {
            logger.LogInformation("Assignments retrieved from cache");
            return OperationResult<List<AssignmentResponseDto>>.Success(mapper.Map<List<AssignmentResponseDto>>(cachedAssignment));
        }
        else
        {
            var queryResult = await assignmentQuery.GetAllAsync(request.filterBy, request.filterOn,
               request.sortBy, request.isAscending, request.pageNumber, request.pageSize, cancellationToken);

            if (queryResult.IsSuccess)
            {
                logger.LogInformation("Assignments retrieved from database");

                memoryCache.Set(new AssignmentCacheKey
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
                   Priority = CacheItemPriority.Normal,
                });
            }

            return queryResult.IsSuccess ? OperationResult<List<AssignmentResponseDto>>.Success(mapper.Map<List<AssignmentResponseDto>>(queryResult.Data))
                : OperationResult<List<AssignmentResponseDto>>.Failure(queryResult.ErrorMessage!);
        }

    }
}
