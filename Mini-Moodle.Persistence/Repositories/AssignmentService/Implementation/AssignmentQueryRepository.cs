using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.AssignmentDto;
using Mini_Moodle.Repositories.AssignmentServices.Interfaces;

namespace Mini_Moodle.Repositories.AssignmentServices.Implementation
{
    public class AssignmentQueryRepository : IAssignmentQueryRepository
    {
        private readonly Moodle_DbContext dbContext;

        public AssignmentQueryRepository(Moodle_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OperationResult<List<Assignment>>> GetAllAsync(string? filterBy, string? filterOn,
            string? sortBy, bool isAscending, int pageNumber, int pageSize,CancellationToken cancellationToken)
        {
            var allAssignments = dbContext.Assignments
                .Include(Assignments => Assignments.Difficulty)
                .AsNoTracking();


            const string byTitle = "TITLE";
            const string byDescription = "DESCRIPTION";

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterBy) && !string.IsNullOrWhiteSpace(filterOn))
            {
                switch (filterBy.ToUpper())
                {
                    case byTitle:
                        allAssignments = allAssignments.Where(c => c.Title.ToLower().Contains(filterOn.ToLower()));
                        break;
                    case byDescription:
                        allAssignments = allAssignments.Where(c => c.Description.ToLower().Contains(filterOn.ToLower()));
                        break;
                    default:
                        return OperationResult<List<Assignment>>.Failure("Invalid filter by parameter");
                }
            }

            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToUpper())
                {
                    case byTitle:
                        allAssignments = isAscending ? allAssignments.OrderBy(c => c.Title) : allAssignments.OrderByDescending(c => c.Title);
                        break;

                    case byDescription:
                        allAssignments = isAscending ? allAssignments.OrderBy(c => c.Description) : allAssignments.OrderByDescending(c => c.Description);
                        break;
                    default:
                        return OperationResult<List<Assignment>>.Failure("Invalid sort by parameter");
                }
            }

            if (pageSize > 0 && pageNumber > 0)
            {
                //Paging
                allAssignments = allAssignments.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

           var listAssignment = await allAssignments.ToListAsync(cancellationToken);

            return OperationResult<List<Assignment>>.Success(listAssignment);
        }

        public async Task<OperationResult<Assignment>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var assignment = await dbContext.Assignments
                .AsNoTracking()
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (assignment == null)
            {
                return OperationResult<Assignment>.Failure("Assignment not Found");
            }

            return OperationResult<Assignment>.Success(assignment);
        }
    }
}
