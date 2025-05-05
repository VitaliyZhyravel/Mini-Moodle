using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.FileStorageServices;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Repositories.SubmissonService.Interfaces;
using System.Security.Claims;

namespace Mini_Moodle.Repositories.SubmissonService.Implementation
{

    public class SubmissionCommandRepository: ISubmissionCommandRepository
    {
        private readonly Moodle_DbContext dbContext;
        private readonly IHttpContextAccessor httpContext;
        private readonly IFileStorageService fileStorage;
        private readonly IConfiguration configuration;

        public SubmissionCommandRepository(Moodle_DbContext dbContext, IHttpContextAccessor httpContext,
            IFileStorageService fileStorage, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.httpContext = httpContext;
            this.fileStorage = fileStorage;
            this.configuration = configuration;
        }

        public async Task<OperationResult<Submission>> CreateAsync(Submission userRequest, CancellationToken cancellationToken)
        {
            var assignment = await dbContext.Assignments.FindAsync(userRequest.AssignmentId, cancellationToken);

            var userIdStr = httpContext?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdStr, out Guid userId)) return OperationResult<Submission>.Failure("Unknown user");

            if (assignment == null)
                return OperationResult<Submission>.Failure("Assignment id is incorrect");

            if (!await dbContext.Users.AnyAsync(x => x.Id == userId))
                return OperationResult<Submission>.Failure("User id is incorrect");

            userRequest.UserId = userId;
            userRequest.DateSubmitted = DateTime.UtcNow;
            userRequest.IsLate = DateTime.UtcNow > assignment.DeadLine;


            await dbContext.Submissions.AddAsync(userRequest, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<Submission>.Success(userRequest);
        }

        public async Task<OperationResult<Guid>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Submissions.FindAsync(id);

            if (entity == null) return OperationResult<Guid>.Failure($"{nameof(Submission)} was not found");

            dbContext.Submissions.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<Guid>.Success(entity.Id);
        }

        public async Task<OperationResult<Submission>> UpdateAsync(Guid id, Submission userRequest, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Submissions.FindAsync(id);

            if (entity == null) return OperationResult<Submission>.Failure($"{nameof(Submission)} was not found");

            entity.Comment = userRequest.Comment;
            await dbContext.SaveChangesAsync();

            return OperationResult<Submission>.Success(entity);
        }

        public async Task<OperationResult<string?>> UploadFileAsync(Guid id, IFormFile file, CancellationToken cancellationToken)
        {
            var submission = await dbContext.Submissions.FindAsync(id, cancellationToken);
            var storageDir = configuration["FileStorage:SubmissionImageDirectory"];

            if (storageDir == null)  return OperationResult<string?>.Failure("Some problems with storage directory");

            if (submission == null)
            {
                return OperationResult<string?>.Failure("Submission not found");
            }

            if (submission.ProjectPath != null)
            {
                return OperationResult<string?>.Failure("File already uploaded");
            }
            
            var rootPath =  await fileStorage.SaveFileAsync(storageDir, file, cancellationToken);

            if (rootPath == null) return OperationResult<string?>.Failure("Some problems with file storage");

            var urlPath = fileStorage.ConvertToUrl(rootPath);

            if (urlPath == null)  return OperationResult<string?>.Failure("Some problems with file storage");
            
            submission.ProjectPath = urlPath;
            await dbContext.SaveChangesAsync(cancellationToken);
            return OperationResult<string?>.Success(urlPath);
        }
    }
}
