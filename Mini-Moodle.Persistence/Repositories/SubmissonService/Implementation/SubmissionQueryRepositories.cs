using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.SubmissionDtos;
using Mini_Moodle.Repositories.SubmissonService.Interfaces;

namespace Mini_Moodle.Repositories.SubmissonService.Implementation
{
    public class SubmissionQueryRepository : ISubmissionQueryRepository
    {
        private readonly Moodle_DbContext dbContext;

        public SubmissionQueryRepository(Moodle_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OperationResult<Submission?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Submissions
                .AsNoTracking()
                .Include(x=>x.Grade)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) return OperationResult<Submission?>.Failure("Submission was not found");

            return OperationResult<Submission?>.Success(entity);
        }
    }
}
