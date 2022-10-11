namespace DotNet_API_Boilerplate.Presentation.Extensions;
using Core.Common.Exceptions;
using Errors;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using DotNet_API_Boilerplate.Core.Common;
using DotNet_API_Boilerplate.Core.Common.Exceptions;

[ExcludeFromCodeCoverage]
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(new ExceptionHandlerOptions()
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    // Set the Http Status Code
                    var statusCode = contextFeature.Error switch
                    {
                        NotFoundException ex => HttpStatusCode.NotFound,
                        ValidationException ex => HttpStatusCode.BadRequest,
                        UserCreateException ex => HttpStatusCode.BadRequest,
                        AuthenticationException ex => HttpStatusCode.OK,
                        _ => HttpStatusCode.InternalServerError
                    };

                    // Prepare Generic Error
                    var apiError = new ApiError(contextFeature.Error.Message, contextFeature.Error.InnerException?.Message, contextFeature.Error.StackTrace);
                    object apiResponse;

                    if (contextFeature.Error is UserCreateException userCreateException)
                    {
                        apiResponse = new ApiResponse<ApiError>()
                        {
                            Status = false,
                            Message = $"{userCreateException.Message} - {string.Join(", ", userCreateException.Errors)}",
                            Object = apiError
                        };
                    }
                    else if (contextFeature.Error is AuthenticationException authenticationException)
                    {
                        apiResponse = new ApiResponse<object>()
                        {
                            Status = false,
                            Message = $"{authenticationException.Message}",
                            Object = null
                        };
                    }
                    else
                    {
                        apiResponse = new ApiResponse<ApiError>()
                        {
                            Status = false,
                            Message = apiError.Message + (!string.IsNullOrEmpty(apiError.InnerMessage) ? $" {apiError.InnerMessage}" : string.Empty),
                            Object = apiError
                        };
                    }

                    // Set Response Details
                    context.Response.StatusCode = (int)statusCode;
                    context.Response.ContentType = "application/json";

                    // Return the Serialized Generic Error
                    await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
                }
            }
        });

        return app;
    }
}
