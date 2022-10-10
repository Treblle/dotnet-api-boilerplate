namespace DotNet_API_Boilerplate.Core.Posts.Commands.UpdatePost;
using FluentValidation;

public class UpdatePostValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostValidator()
    {
        RuleFor(r => r.Id).NotEqual(Guid.Empty).WithMessage("A post id was not supplied to Update the post.");
        RuleFor(r => r.Title).NotEqual(string.Empty).WithMessage("Post title was not supplied.");
    }
}
