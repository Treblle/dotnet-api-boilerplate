namespace DotNet_API_Boilerplate.Core.Posts.Commands.DeletePost;
using FluentValidation;

public class DeletePostValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostValidator()
    {
        RuleFor(r => r.Id).NotEqual(Guid.Empty).WithMessage("A post Id was not supplied.");
    }
}
