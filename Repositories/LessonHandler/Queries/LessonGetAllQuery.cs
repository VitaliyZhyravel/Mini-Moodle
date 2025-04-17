using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.Lessons;

namespace Mini_Moodle.Repositories.LessonHandler.Queries;

public record LessonGetAllQuery([FromQuery] string? filterBy = null, [FromQuery] string? filterOn = null,
            [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true, int pageNumber = 0, int pageSize = 1000) : IRequest<OperationResult<List<LessonResponseDto>>> { }
