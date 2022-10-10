namespace Treblle_Core_API_Boilerplate.Core.Auth.Commands.Login;
using MediatR;
using Treblle_Core_API_Boilerplate.Core.Auth.Dto;

public class LoginCommand : IRequest<TokenDto>
{
    public string Username { get; init; }
    public string Password { get; init; }
}
