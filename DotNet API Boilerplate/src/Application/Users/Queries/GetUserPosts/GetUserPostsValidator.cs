namespace Treblle_Core_API_Boilerplate.Core.Users.Queries.GetUserPosts;
using FluentValidation;

public class GetUserPostsValidator : AbstractValidator<GetUserPostsQuery>
{
    public GetUserPostsValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEqual(Guid.Empty).WithMessage("User Id was not supplied.");
    }
}
