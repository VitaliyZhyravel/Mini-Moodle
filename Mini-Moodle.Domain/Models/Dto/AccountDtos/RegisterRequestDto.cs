
namespace Mini_Moodle.Models.Dto.Account;

public class RegisterRequestDto
{
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
