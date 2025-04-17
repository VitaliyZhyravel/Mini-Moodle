using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Models.Dto.Lessons;
using Mini_Moodle.Repositories.Courses.Queries;

namespace Mini_Moodle.Repositories.LessonHandler.Queries;

public class LessonGetByIdQueryHandler : IRequestHandler<LessonGetByIdQuery,OperationResult<LessonResponseDto>>
{

    public Moodle_DbContext dbContext { get; }
    public IMapper mapper { get; }

    public LessonGetByIdQueryHandler(Moodle_DbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<OperationResult<LessonResponseDto>> Handle(LessonGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await dbContext.Lessons.AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == request.id, cancellationToken);

        if (result == null)
        {
            return OperationResult<LessonResponseDto>.Failure("Course not found");
        }

        var courseDto = mapper.Map<LessonResponseDto>(result);
        return OperationResult<LessonResponseDto>.Success(courseDto);
    }
}
