using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Repositories.UserService.Interfaces;
using System.Security.Claims;

namespace Mini_Moodle.Repositories.UserService.Implementation;

public class UserQueryRepository : IUserQueryRepository
{
    private readonly Moodle_DbContext dbContext;
    private readonly IHttpContextAccessor httpContext;

    public UserQueryRepository(Moodle_DbContext dbContext, IHttpContextAccessor httpContext)
    {
        this.dbContext = dbContext;
        this.httpContext = httpContext;
    }

    public async Task<OperationResult<List<Submission>>> GetSubmissionsAsync(CancellationToken cancellationToken)
    {
        var userIdStr = httpContext?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdStr == null) return OperationResult<List<Submission>>.Failure("User not found");

        if (!Guid.TryParse(userIdStr, out Guid userId)) return OperationResult<List<Submission>>.Failure("User not found");

        var entity = await dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.Assignment)
            .Include(x => x.Grade)
            .Where(a => a.UserId == userId)
            .ToListAsync();

        if (entity.Count == 0) return OperationResult<List<Submission>>.Failure("User dont have submission");

        return OperationResult<List<Submission>>.Success(entity);
    }
}
