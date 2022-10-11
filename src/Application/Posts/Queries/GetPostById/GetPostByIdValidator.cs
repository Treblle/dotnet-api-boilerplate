namespace DotNet_API_Boilerplate.Core.Posts.Queries.GetPostById;
using FluentValidation;

public class GetPostByIdValidator : AbstractValidator<GetPostByIdQuery>
{
    public GetPostByIdValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEqual(Guid.Empty).WithMessage("A Post Id was not supplied.");
    }
}
