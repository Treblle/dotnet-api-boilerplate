namespace Treblle_Core_API_Boilerplate.Core.Common.Services;

using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Services;

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
