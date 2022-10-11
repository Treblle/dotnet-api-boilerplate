namespace DotNet_API_Boilerplate.Core.Auth.Commands.Login;
using MediatR;
using DotNet_API_Boilerplate.Core.Auth.Dto;

public class LoginCommand : IRequest<TokenDto>
{
    public string Username { get; init; }
    public string Password { get; init; }
}
