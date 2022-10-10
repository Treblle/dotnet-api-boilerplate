namespace Treblle_Core_API_Boilerplate.Presentation.Endpoints.Auth;
using Errors;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using Commands = Core.Auth.Commands;
using Treblle_Core_API_Boilerplate.Presentation.Endpoints.Auth.Requests;
using Treblle.Net.Core;
using Asp.Versioning.Builder;
using Treblle_Core_API_Boilerplate.Presentation.Versioning;
using Treblle_Core_API_Boilerplate.Core.Auth.Dto;
using Treblle_Core_API_Boilerplate.Core.Common;

[ExcludeFromCodeCoverage]
public static class AuthEndpoints
{
    public static WebApplication MapAuthEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {

        app.MapPost(
               "/api/login",
               async (IMediator mediator, HttpRequest httpRequest, LoginRequest request) =>
               {
                   var command = new Commands.Login.LoginCommand()
                   {
                       Username = request.Username,
                       Password = request.Password
                   };
                   return Results.Ok(new ApiResponse<TokenDto>(true, string.Empty, await mediator.Send(command)));
               })
            .UseTreblle()
            .AllowAnonymous()
            .WithTags("Auth")
            .WithMetadata(new SwaggerOperationAttribute("Login with user credentials", "\n    POST /Login\n     {         \"username\": \"john.doe@gmail.com\",         \"password\": \"password\"    }"))
            .Produces<ApiResponse<TokenDto>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion1_0);

        return app;
    }
}
