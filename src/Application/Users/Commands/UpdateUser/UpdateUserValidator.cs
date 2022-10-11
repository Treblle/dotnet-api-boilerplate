namespace DotNet_API_Boilerplate.Core.Users.Commands.UpdateUser;
using FluentValidation;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(r => r.Email).NotEqual(string.Empty).WithMessage("Email was not supplied.");
        RuleFor(r => r.CurrentPassword).NotEqual(string.Empty).WithMessage("Current password was not supplied.");
        RuleFor(r => r.NewPassword).NotEqual(string.Empty).WithMessage("New password was not supplied.");
    }
}

