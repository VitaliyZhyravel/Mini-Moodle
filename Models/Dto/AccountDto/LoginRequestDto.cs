using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Dto.Account;

public class LoginRequestDto
{
    [Required(ErrorMessage = "{0} Can't be empty")]
    [EmailAddress(ErrorMessage = "Incorrect {0} format")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "{0} Can't be empty")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
