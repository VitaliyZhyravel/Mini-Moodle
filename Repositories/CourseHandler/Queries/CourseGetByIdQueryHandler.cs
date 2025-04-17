using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.Courses.Queries
{
    public class CourseGetByIdQueryHandler : IRequestHandler<CourseGetByIdQuery,OperationResult<CourseResponseDto>>
    {
        public Moodle_DbContext dbContext { get; }
        public IMapper mapper { get; }

        public CourseGetByIdQueryHandler(Moodle_DbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

       

        public async Task<OperationResult<CourseResponseDto>> Handle(CourseGetByIdQuery request, CancellationToken cancellationToken)
        {
         var result =  await dbContext.Courses.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.id,cancellationToken);

            if (result == null)
            {
                return OperationResult<CourseResponseDto>.Failure("Course not found");
            }

           var courseDto = mapper.Map<CourseResponseDto>(result);
            return OperationResult<CourseResponseDto>.Success(courseDto);
        }
    }
}
