namespace DotNet_API_Boilerplate.Core.Users.Commands.CreateUser;
using MediatR;
using DotNet_API_Boilerplate.Core.Users.Dto;

public class CreateUserCommand : IRequest<UserDto>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}
