using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Repositories.AccountService.Command.Login
{
    public class AccountLoginCommandValidator : AbstractValidator<AccountLoginCommand>
    {
        public AccountLoginCommandValidator()
        {
                RuleFor(x => x.loginRequest.Email)
                 .EmailAddress()
                 .WithMessage("Invalid email format")
                 .NotEmpty().WithMessage("Emai can't be empty");

            RuleFor(x => x.loginRequest.Password)
                .NotEmpty()
                .WithMessage("Password can`t be empty");
        }
    }
}
