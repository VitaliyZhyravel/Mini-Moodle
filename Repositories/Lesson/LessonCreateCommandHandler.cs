using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.Lesson;

public class LessonCreateCommandHandler : IRequestHandler<LessonCreateCommand, OperationResult<LessonResponseToCreateUpdateDto>>
{
    private readonly Moodle_DbContext dbContext;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContext;

    public LessonCreateCommandHandler(Moodle_DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.httpContext = httpContext;
    }

    public async Task<OperationResult<LessonResponseToCreateUpdateDto>> Handle(LessonCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!IsValid(request.request.File))
            {
                return OperationResult<LessonResponseToCreateUpdateDto>.Failure("file is not valid");
            }
        }
        catch (Exception ex)
        {
            return OperationResult<LessonResponseToCreateUpdateDto>.Failure(ex.Message);
        }

        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Videos", request.request.File.FileName);

        using (FileStream fileStream = new FileStream(path, FileMode.Create))
        {
            await request.request.File.CopyToAsync(fileStream);
        }

        var requestHost = httpContext.HttpContext.Request;
        string videoUrl = $"{requestHost.Scheme}://{requestHost.Host}/Videos/{request.request.File.FileName}";

        var newLesson = mapper.Map<Models.Domain.Lesson>(request.request);
        newLesson.VideoUrl = videoUrl;

        await dbContext.Lessons.AddAsync(newLesson);
        await dbContext.SaveChangesAsync(cancellationToken);


        return OperationResult<LessonResponseToCreateUpdateDto>.Success(mapper.Map<LessonResponseToCreateUpdateDto>(newLesson));
    }

    public bool IsValid(IFormFile formFile)
    {
        if (formFile == null || formFile.Length > 10485760)
        {
            throw new InvalidDataException("File is empty or exceeds 10MB");
        }

        List<string> availableExtensions = new List<string> { ".mp4", ".mov" };

        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Videos", formFile.FileName);

        if (!availableExtensions.Any(x => x == Path.GetExtension(path)))
        {
            throw new InvalidDataException("File extension is not valid");
        }
        return true;
    }
}
