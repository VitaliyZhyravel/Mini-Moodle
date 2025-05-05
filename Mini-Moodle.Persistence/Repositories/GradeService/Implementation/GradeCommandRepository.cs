using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.GradeDto;
using Mini_Moodle.Repositories.GradeService.Interfaces;

namespace Mini_Moodle.Repositories.GradeService.Implementation
{
    public class GradeCommandRepository : IGradeCommandRepository
    {
        private readonly Moodle_DbContext dbContext;

        public GradeCommandRepository(Moodle_DbContext dbContext )
        {
            this.dbContext = dbContext;
        }

        public async Task<OperationResult<Grade>> CreateAsync(Grade request, CancellationToken cancellationToken)
        {
            var entitySubmission = await dbContext.Submissions
                .FirstOrDefaultAsync(x => x.Id == request.SubmissionId, cancellationToken);

            if (entitySubmission == null) return OperationResult<Grade>.Failure("Submission was not found");

            if (entitySubmission.IsLate)
                request.Score -= request.Score / 100 * 30;

            entitySubmission.TotalGrade = request.Score;

            await dbContext.Grades.AddAsync(request);
            await dbContext.SaveChangesAsync(cancellationToken);

            return OperationResult<Grade>.Success(request);
        }
    }
}
