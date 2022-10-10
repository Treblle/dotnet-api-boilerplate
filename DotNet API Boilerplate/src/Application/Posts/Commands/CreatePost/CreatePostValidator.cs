namespace Treblle_Core_API_Boilerplate.Core.Posts.Commands.CreatePost;
using FluentValidation;

public class CreatePostValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostValidator()
    {
        RuleFor(r => r.Title).NotEqual(string.Empty).WithMessage("Post title was not supplied.");
    }
}
