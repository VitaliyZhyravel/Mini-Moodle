using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini_Moodle.Exceptions;
using Mini_Moodle.Models.Dto.AssignmentDto;

namespace Mini_Moodle.Repositories.AssignmentsService.Queries.GetAllAssignment;

public record AssignmentGetAllQuery(string? filterBy, string? filterOn,
            string? sortBy, bool isAscending, int pageNumber, int pageSize) : IRequest<OperationResult<List<AssignmentResponseDto>>>
{ }
