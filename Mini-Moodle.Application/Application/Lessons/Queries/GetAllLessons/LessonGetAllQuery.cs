using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.LessonService.Queries.GetAllLessons;

public record LessonGetAllQuery(string? filterBy, string? filterOn,
            string? sortBy, bool isAscending, int pageNumber, int pageSize) : IRequest<OperationResult<List<LessonResponseDto>>> { }
