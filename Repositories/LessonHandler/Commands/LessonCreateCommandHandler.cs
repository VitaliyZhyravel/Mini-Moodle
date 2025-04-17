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
        var newLesson = mapper.Map<Models.Domain.Lesson>(request.request);

        newLesson.CourseId = request.request.CourseId;

        await dbContext.Lessons.AddAsync(newLesson);
        await dbContext.SaveChangesAsync(cancellationToken);

        return OperationResult<LessonResponseToCreateUpdateDto>.Success(mapper.Map<LessonResponseToCreateUpdateDto>(newLesson));
    }
}
