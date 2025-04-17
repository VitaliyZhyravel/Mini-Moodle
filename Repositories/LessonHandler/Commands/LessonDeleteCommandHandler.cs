using AutoMapper;
using MediatR;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.LessonHandler.Commands
{
    public class LessonDeleteCommandHandler : IRequestHandler<LessonDeleteCommand, OperationResult<string>>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly IMapper mapper;

        public LessonDeleteCommandHandler(Moodle_DbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<OperationResult<string>> Handle(LessonDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Lessons.FindAsync(request.id,cancellationToken);

            if (entity == null)
            {
                return OperationResult<string>.Failure("Lesson not found");
            }

            dbContext.Lessons.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<string>.Success("Lesson deleted successfully");
        }
    }
}
