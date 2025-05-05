using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;

using Mini_Moodle.Repositories.AssignmentServices.Interfaces;

namespace Mini_Moodle.Repositories.AssignmentServices.Implementation;

public class AssignmentCommandRepository : IAssignmentCommandRepository
{
    private readonly Moodle_DbContext dbContext;

    public AssignmentCommandRepository(Moodle_DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<OperationResult<Assignment>> CreateAsync(Assignment userRequest, CancellationToken cancellationToken)
    {
        if (!await dbContext.Lessons.AnyAsync(x => userRequest.LessonId == x.Id, cancellationToken))
        {
            return OperationResult<Assignment>.Failure("Lesson id is incorrect");
        }

        var difficulty = await dbContext.Difficulties.FindAsync(userRequest.DifficultyId);

        if (difficulty == null)
        {
            return OperationResult<Assignment>.Failure("Difficulty id is incorrect");
        }

        userRequest.DeadLine = DateTime.UtcNow.AddDays(difficulty.DaysToExpire).Date.AddHours(23).AddMinutes(59);

        await dbContext.Assignments.AddAsync(userRequest, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return OperationResult<Assignment>.Success(userRequest);
    }

    public async Task<OperationResult<Guid>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Assignments.FindAsync(id, cancellationToken);

        if (entity == null)
        {
            return OperationResult<Guid>.Failure("Assignment not found");
        }
        dbContext.Assignments.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return OperationResult<Guid>.Success(entity.Id);
    }

    public async Task<OperationResult<Assignment>> UpdateAsync(Guid id, Assignment userRequest, double AddMinutesToDeadLine, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Assignments
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
        {
            return OperationResult<Assignment>.Failure("Assignment not found");
        }

        entity.Title = userRequest.Title;
        entity.Description = userRequest.Description;
        entity.DeadLine = entity.DeadLine.AddMinutes(AddMinutesToDeadLine);

        await dbContext.SaveChangesAsync(cancellationToken);

        return OperationResult<Assignment>.Success(entity);
    }
}
