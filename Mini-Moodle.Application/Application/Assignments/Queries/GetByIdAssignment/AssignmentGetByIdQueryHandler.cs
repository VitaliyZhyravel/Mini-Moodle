using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.AssignmentDto;
using Mini_Moodle.Repositories.AssignmentServices.Interfaces;

namespace Mini_Moodle.Repositories.AssignmentsService.Queries.GetByIdAssignment;

public class AssignmentGetByIdQueryHandler : IRequestHandler<AssignmentGetByIdQuery, OperationResult<AssignmentResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IAssignmentQueryRepository assignmentQuery;
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<AssignmentGetByIdQueryHandler> logger;

    public AssignmentGetByIdQueryHandler( IMapper mapper,IAssignmentQueryRepository assignmentQuery,IMemoryCache memoryCache,ILogger<AssignmentGetByIdQueryHandler> logger)
    {
        this.mapper = mapper;
        this.assignmentQuery = assignmentQuery;
        this.memoryCache = memoryCache;
        this.logger = logger;
    }
    public async Task<OperationResult<AssignmentResponseDto>> Handle(AssignmentGetByIdQuery request, CancellationToken cancellationToken)
    {
        memoryCache.TryGetValue(request.id, out Assignment? cachedAssignment);

        if (cachedAssignment != null)
        {
            logger.LogInformation($"Assignment retrieved from cache");
            return OperationResult<AssignmentResponseDto>.Success(mapper.Map<AssignmentResponseDto>(cachedAssignment));
        }
        else
        {
            var queryResult = await assignmentQuery.GetByIdAsync(request.id, cancellationToken);

            if (queryResult.IsSuccess)
            {
                logger.LogInformation($"Assignment retrieved from database");
                memoryCache.Set(request.id, queryResult.Data, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(3600),
                    SlidingExpiration = TimeSpan.FromSeconds(45),
                    Priority = CacheItemPriority.Normal,
                });
            }

            return queryResult.IsSuccess ? OperationResult<AssignmentResponseDto>.Success(mapper.Map<AssignmentResponseDto>(queryResult.Data))
                : OperationResult<AssignmentResponseDto>.Failure(queryResult.ErrorMessage!);
        }
    }
}
