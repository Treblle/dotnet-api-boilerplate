namespace DotNet_API_Boilerplate.Core.Auth.Commands.Login;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotNet_API_Boilerplate.Core.Common.Exceptions;
using DotNet_API_Boilerplate.Core.Auth.Dto;
using DotNet_API_Boilerplate.Core.Common.Configuration;
using DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;
using DotNet_API_Boilerplate.Core.Users.Entities;
using static DotNet_API_Boilerplate.Core.Common.Helpers.Constants.Strings;

public class LoginHandler : IRequestHandler<LoginCommand, TokenDto>
{
    private readonly IUserRepository _userRepository;
    private readonly JwtConfiguration _jwtConfiguration;

    public LoginHandler(IUserRepository userRepository, JwtConfiguration jwtConfiguration)
    {
        _userRepository = userRepository;
        _jwtConfiguration = jwtConfiguration;
    }

    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userValid = await _userRepository.ValidateUserAsync(request.Username, request.Password, cancellationToken);
        if (!userValid)
        {
            throw new AuthenticationException("Could not authenticate using the provided credentials.");
        }
        var user = await _userRepository.GetUserByUsername(request.Username);
        return CreateTokenAsync(user);
    }

    private TokenDto CreateTokenAsync(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new TokenDto()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
            ValidTo = tokenOptions.ValidTo
        };
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtConfiguration.Key);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private static List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtClaimIdentifiers.Id, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.ExpiresIn)),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}
