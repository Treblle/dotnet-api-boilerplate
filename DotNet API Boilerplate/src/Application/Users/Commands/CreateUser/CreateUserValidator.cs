namespace DotNet_API_Boilerplate.Core.Users.Commands.CreateUser;
using FluentValidation;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(r => r.Email).NotEqual(string.Empty).WithMessage("Email was not supplied.");
        RuleFor(r => r.Password).NotEqual(string.Empty).WithMessage("Password was not supplied.");
    }
}
