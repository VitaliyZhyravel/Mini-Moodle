using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Repositories.AccountService.Command.Register
{
    public class AccountRegisterCommandValidator : AbstractValidator<AccountRegisterCommand>
    {
        public AccountRegisterCommandValidator()
        {
            RuleFor(x => x.registerRequest.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(40)
                .WithMessage("Name can contain no more than 40 characters"); ;

            RuleFor(x => x.registerRequest.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.registerRequest.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^\+380\d+$")
                .WithMessage("Phone number contain only digits")
                .MaximumLength(13)
                .WithMessage("Phone number can contain no more than 13 characters");

            RuleFor(x => x.registerRequest.Password)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$")
                .WithMessage("Password must contain at least 8 characters, including at least one uppercase letter, one lowercase letter, one digit, and one special character.");

            RuleFor(x => x.registerRequest.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password is required.")
                .Equal(x => x.registerRequest.Password)
                .WithMessage("The password and confirmation password do not match.");
        }
    }
}
