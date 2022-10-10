namespace Treblle_Core_API_Boilerplate.Core;
using Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Treblle_Core_API_Boilerplate.Core.Common.Behaviours;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Services;
using Treblle_Core_API_Boilerplate.Core.Common.Services;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient<IHttpContextService, HttpContextService>();
        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}
