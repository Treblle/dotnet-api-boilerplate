namespace DotNet_API_Boilerplate.Core.Users.Queries.GetUserById;
using FluentValidation;

public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEqual(Guid.Empty).WithMessage("User Id was not supplied.");
    }
}
