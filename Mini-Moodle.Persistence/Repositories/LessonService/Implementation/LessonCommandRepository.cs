using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.FileStorageServices;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Repositories.LessonService.Interfaces;

namespace Mini_Moodle.Repositories.LessonService.Implementation
{
    public class LessonCommandRepository : ILessonCommandRepository
    {
        private readonly Moodle_DbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly IFileStorageService fileService;

        public LessonCommandRepository(Moodle_DbContext dbContext,IConfiguration configuration, IFileStorageService fileService)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.fileService = fileService;
        }

        public async Task<OperationResult<Lesson>> CreateAsync(Lesson userRequest, CancellationToken cancellationToken)
        {
            await dbContext.Lessons.AddAsync(userRequest);
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<Lesson>.Success(userRequest);
        }

        public async Task<OperationResult<Guid>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Lessons.FindAsync(id, cancellationToken);

            if (entity == null)
            {
                return OperationResult<Guid>.Failure("Lesson not found");
            }

            dbContext.Lessons.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<Guid>.Success(entity.Id);
        }

        public async Task<OperationResult<Lesson>> UpdateAsync(Guid id, Lesson userRequest, CancellationToken cancellationToken)
        {
            var lesson = await dbContext.Lessons.FindAsync(id, cancellationToken);
            if (lesson == null)
            {
                return OperationResult<Lesson>.Failure("Lesson not found");
            }

            if (dbContext.Courses.Any(x => x.Id == userRequest.CourseId))
                lesson.CourseId = userRequest.CourseId;

            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<Lesson>.Success(lesson);
        }

        public async Task<OperationResult<string?>> UploadFileAsync(Guid id, IFormFile file, CancellationToken cancellationToken)
        {
            string storageDir = configuration["FileStorage:LessonVideoDirectory"]!;
            var lesson = await dbContext.Lessons
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (string.IsNullOrEmpty(storageDir))
            {
                return OperationResult<string?>.Failure("Some error wiht storage directory");
            }

            if (lesson?.VideoUrl != null)
            {
                return OperationResult<string?>.Failure("Video already uploaded");
            }

            string? rootPath = await fileService.SaveFileAsync(storageDir, file, cancellationToken);

            if (rootPath == null)
            {
                return OperationResult<string?>.Failure("Root path is null");
            }


            var urlPath = fileService.ConvertToUrl(rootPath);

            lesson.VideoUrl = urlPath;
            await dbContext.SaveChangesAsync(cancellationToken);
            return OperationResult<string?>.Success(urlPath);
        }
    }
}
