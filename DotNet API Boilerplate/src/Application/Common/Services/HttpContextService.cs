namespace Treblle_Core_API_Boilerplate.Core.Common.Services;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Services;
using static Treblle_Core_API_Boilerplate.Core.Common.Helpers.Constants.Strings;

public class HttpContextService : IHttpContextService
{
    #region Ctors and Members

    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    #endregion Ctors and Members

    public string CurrentUserId => _contextAccessor.HttpContext?.User?.Claims?
            .FirstOrDefault(c => c.Type == JwtClaimIdentifiers.Id)?.Value;
}
