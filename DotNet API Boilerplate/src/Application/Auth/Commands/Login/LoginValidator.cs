namespace DotNet_API_Boilerplate.Core.Auth.Commands.Login;

using FluentValidation;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(r => r.Username).NotEqual(string.Empty).WithMessage("Username was not supplied.");
        RuleFor(r => r.Password).NotEqual(string.Empty).WithMessage("Password was not supplied.");
    }
}
