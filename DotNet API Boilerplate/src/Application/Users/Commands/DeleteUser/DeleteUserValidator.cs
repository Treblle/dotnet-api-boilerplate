namespace DotNet_API_Boilerplate.Core.Users.Commands.DeleteUser;
using FluentValidation;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(r => r.Id).NotEqual(Guid.Empty).WithMessage("A User Id was not supplied.");
    }
}
