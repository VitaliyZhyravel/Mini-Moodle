
using Microsoft.AspNetCore.Http;

namespace Mini_Moodle.Models.Dto.SubmissionDtos;

public class SubmissionFileUploadDto
{
    public IFormFile Files { get; set; }
}
