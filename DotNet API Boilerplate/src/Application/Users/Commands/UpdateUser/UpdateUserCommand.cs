namespace Treblle_Core_API_Boilerplate.Core.Users.Commands.UpdateUser;
using MediatR;
using Treblle_Core_API_Boilerplate.Core.Users.Dto;

public class UpdateUserCommand : IRequest<UserDto>
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string CurrentPassword { get; init; }
    public string NewPassword { get; init; }
}

