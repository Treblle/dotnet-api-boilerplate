namespace DotNet_API_Boilerplate.Core.Common.Services;

using DotNet_API_Boilerplate.Core.Common.Contracts.Services;

internal class AuthService : IAuthService
{
    private readonly IHttpContextService _httpContextService;

    public AuthService(IHttpContextService httpContextService)
    {
        _httpContextService = httpContextService;
    }

    public string GetCurrentUserIdentityId()
    {
        return _httpContextService.CurrentUserId;
    }
}
