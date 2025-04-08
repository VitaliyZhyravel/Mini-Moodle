using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Models.Dto;

namespace Mini_Moodle.Repositories.Courses.Queries
{
    public record CourseGetAllQuery(string? filterBy, string? filterOn,
            string? sortBy, bool isAscending, int pageNumber, int pageSize) : IRequest<List<CourseResponseDto>> { }
}
