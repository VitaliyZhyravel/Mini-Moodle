using MediatR;
using Microsoft.AspNetCore.Http;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.Lesson
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, OperationResult<UploadVideoResponseDto>>
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly Moodle_DbContext dbContext;

        public UploadVideoCommandHandler(IHttpContextAccessor httpContext, Moodle_DbContext dbContext)
        {
            this.httpContext = httpContext;
            this.dbContext = dbContext;
        }
        public async Task<OperationResult<UploadVideoResponseDto>> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            string videosDir = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "Videos");
            if (!Directory.Exists(videosDir)) 
            {
                var info = Directory.CreateDirectory(videosDir);
            }
              
            var extension = Path.GetExtension(request.videoFile.FileName);
            var fileName = $"{Guid.NewGuid()}";
            string path =  Path.Combine(Directory.GetCurrentDirectory(), videosDir, $"{ fileName}{extension}");

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await request.videoFile.CopyToAsync(fileStream);
            }

            var requestHost = httpContext.HttpContext.Request;
            string videoUrl = $"{requestHost.Scheme}://{requestHost.Host}/Videos/{fileName}{extension}";

            var lesson = await dbContext.Lessons.FindAsync(request.id, cancellationToken);

            if (lesson == null)
            {
                return OperationResult<UploadVideoResponseDto>.Failure("Lesson not found");
            }

            if (lesson.VideoUrl != null)
            {
                return OperationResult<UploadVideoResponseDto>.Failure("Video already uploaded");
            }

            lesson.VideoUrl = videoUrl;
            await dbContext.SaveChangesAsync(cancellationToken);
            return OperationResult<UploadVideoResponseDto>.Success(new UploadVideoResponseDto
            {
                LessonId = request.id,
                Message = "Video uploaded successfully",
                VideoUrl = videoUrl,
            });
        }
    }
}
