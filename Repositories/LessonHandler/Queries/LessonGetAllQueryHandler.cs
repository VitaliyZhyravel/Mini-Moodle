using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Domain;
using Mini_Moodle.Models.Dto.Course;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.LessonHandler.Queries
{
    public class LessonGetAllQueryHandler : IRequestHandler<LessonGetAllQuery, OperationResult<List<LessonResponseDto>>>
    {
        private readonly Moodle_DbContext dbContext;
        private readonly IMapper mapper;

        public LessonGetAllQueryHandler(Moodle_DbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<OperationResult<List<LessonResponseDto>>> Handle(LessonGetAllQuery request, CancellationToken cancellationToken)
        {
            List<Models.Domain.Lesson>? allLessons = await dbContext.Lessons
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            //Filtering
            if (!string.IsNullOrWhiteSpace(request.filterBy) && !string.IsNullOrWhiteSpace(request.filterOn))
            {
                switch (request.filterBy)
                {
                    case nameof(Course.Title):
                        allLessons = allLessons.Where(c => c.Title.Contains(request.filterOn)).ToList();
                        break;

                    case nameof(Course.Description):
                        allLessons = allLessons.Where(c => c.Description?.Contains(request.filterOn) ?? false).ToList();
                        break;
                }
            }
            //Sorting
            if (!string.IsNullOrWhiteSpace(request.sortBy))
            {
                switch (request.sortBy)
                {
                    case nameof(Course.Title):
                        allLessons = request.isAscending ? allLessons.OrderBy(c => c.Title).ToList() : allLessons.OrderByDescending(c => c.Title).ToList();
                        break;

                    case nameof(Course.Description):
                        allLessons = request.isAscending ? allLessons.OrderBy(c => c.Description).ToList() : allLessons.OrderByDescending(c => c.Description).ToList();
                        break;
                }
            }
            //Paging
            allLessons = allLessons.Skip((request.pageNumber - 1) * request.pageSize).Take(request.pageSize).ToList();

            return OperationResult<List<LessonResponseDto>>.Success(mapper.Map<List<LessonResponseDto>>(allLessons));
        }
    }
}
