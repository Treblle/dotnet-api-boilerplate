namespace DotNet_API_Boilerplate.Core.Common.Contracts.Services;

public interface IHttpContextService
{
    string CurrentUserId { get; }
}
