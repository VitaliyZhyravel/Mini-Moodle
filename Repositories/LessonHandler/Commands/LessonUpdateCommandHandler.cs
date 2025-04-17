using AutoMapper;
using MediatR;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.LessonHandler.Commands
{
    public class LessonUpdateCommandHandler : IRequestHandler<LessonUpdateCommand, OperationResult<LessonResponseToCreateUpdateDto>>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly IMapper mapper;

        public LessonUpdateCommandHandler(Moodle_DbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<OperationResult<LessonResponseToCreateUpdateDto>> Handle(LessonUpdateCommand request, CancellationToken cancellationToken)
        {
            var lesson = await dbContext.Lessons.FindAsync(request.id, cancellationToken);
            if (lesson == null)
            {
                return OperationResult<LessonResponseToCreateUpdateDto>.Failure("Lesson not found");
            }

            if (dbContext.Courses.Any(x => x.Id == request.userRequest.CourseId))
                lesson.CourseId = request.userRequest.CourseId;
            lesson.Title = request.userRequest.Title;
            lesson.Description = request.userRequest.Description;

            await dbContext.SaveChangesAsync(cancellationToken);
            return OperationResult<LessonResponseToCreateUpdateDto>.Success(mapper.Map<LessonResponseToCreateUpdateDto>(lesson));
        }
    }
}
