using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Course;

namespace Mini_Moodle.Repositories.Courses.Queries
{
    public record CourseGetAllQuery(string? filterBy, string? filterOn,
            string? sortBy, bool isAscending, int pageNumber, int pageSize) :
        IRequest<OperationResult<List<CourseResponseDto>>>
    { }




}
