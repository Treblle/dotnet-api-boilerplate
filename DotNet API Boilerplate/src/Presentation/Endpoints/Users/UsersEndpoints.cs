namespace Treblle_Core_API_Boilerplate.Presentation.Endpoints.Users;
using Errors;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using Queries = Core.Users.Queries;
using Commands = Core.Users.Commands;
using Treblle_Core_API_Boilerplate.Presentation.Endpoints.Users.Requests;
using Microsoft.AspNetCore.Http.Extensions;
using Treblle.Net.Core;
using Asp.Versioning.Builder;
using Treblle_Core_API_Boilerplate.Presentation.Versioning;
using Treblle_Core_API_Boilerplate.Core.Common;
using Treblle_Core_API_Boilerplate.Core.Users.Dto;
using Treblle_Core_API_Boilerplate.Core.Posts.Dto;

[ExcludeFromCodeCoverage]
public static class UsersEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("/api/users",
               async (IMediator mediator) =>
               {
                   var result = await mediator.Send(new Queries.GetUsers.GetUsersQuery());
                   return Results.Ok(new ApiResponse<List<UserDto>>(true, string.Empty, result));
               })
           .UseTreblle()
           .RequireAuthorization()
           .WithTags("Users")
           .WithMetadata(new SwaggerOperationAttribute("Lookup all users", "\n    GET /Users"))
           .Produces<ApiResponse<List<UserDto>>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
           .WithApiVersionSet(apiVersionSet)
           .MapToApiVersion(ApiVersions.ApiVersion1_0);

        app.MapGet(
               "/api/users/{id:guid}",
               async (IMediator mediator, Guid id) =>
               {
                   var result = await mediator.Send(new Queries.GetUserById.GetUserByIdQuery { Id = id });
                   return Results.Ok(new ApiResponse<UserDto>(true, string.Empty, result));
               })
           .UseTreblle()
           .RequireAuthorization()
           .WithTags("Users")
           .WithMetadata(new SwaggerOperationAttribute("Lookup a User by their Id", "\n    GET /Users/00000000-0000-0000-0000-000000000000"))
           .Produces<ApiResponse<UserDto>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiResponse<ApiError>>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
           .WithApiVersionSet(apiVersionSet)
           .MapToApiVersion(ApiVersions.ApiVersion1_0);

        app.MapGet(
               "/api/users/{id:guid}/posts",
               async (IMediator mediator, Guid id) =>
               {
                   var result = await mediator.Send(new Queries.GetUserPosts.GetUserPostsQuery { Id = id });
                   return Results.Ok(new ApiResponse<List<PostDto>>(true, string.Empty, result));
               })
           .UseTreblle()
           .RequireAuthorization()
           .WithTags("Users")
           .WithMetadata(new SwaggerOperationAttribute("Get all posts by User Id", "\n    GET /Users/00000000-0000-0000-0000-000000000000/posts"))
           .Produces<ApiResponse<List<PostDto>>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiResponse<ApiError>>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
           .WithApiVersionSet(apiVersionSet)
           .MapToApiVersion(ApiVersions.ApiVersion1_0);

        app.MapPost(
              "/api/users",
              async (IMediator mediator, HttpRequest httpRequest, CreateUserRequest request) =>
              {
                  var command = new Commands.CreateUser.CreateUserCommand()
                  {
                      FirstName = request.FirstName,
                      LastName = request.LastName,
                      Email = request.Email,
                      Password = request.Password
                  };
                  var result = await mediator.Send(command);
                  return Results.Created(UriHelper.GetEncodedUrl(httpRequest), new ApiResponse<UserDto>(true, "User created successfully.", result));
              })
           .UseTreblle()
           .RequireAuthorization()
           .WithTags("Users")
           .WithMetadata(new SwaggerOperationAttribute("Create a User", "\n    POST /Users\n     {         \"firstName\": \"John\",         \"lastName\": \"Doe\",         \"email\": \"john.doe@gmail.com\",         \"password\": \"password\"    }"))
           .Produces<ApiResponse<UserDto>>(StatusCodes.Status201Created, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
           .WithApiVersionSet(apiVersionSet)
           .MapToApiVersion(ApiVersions.ApiVersion1_0);


        app.MapPut("/api/users/{id:guid}",
                async (IMediator mediator, Guid id, UpdateUserRequest request) =>
                {
                    var command = new Commands.UpdateUser.UpdateUserCommand()
                    {
                        Id = id,
                        Email = request.Email,
                        CurrentPassword = request.CurrentPassword,
                        NewPassword = request.NewPassword,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                    };

                    var result = await mediator.Send(command);

                    return Results.Ok(new ApiResponse<UserDto>(true, "User updated successfully", result));
                })
            .UseTreblle()
            .WithTags("Users")
            .RequireAuthorization()
            .WithMetadata(new SwaggerOperationAttribute("Update a User", "\n    PUT /Users/00000000-0000-0000-0000-000000000000\n      {         \"firstName\": \"John\",         \"lastName\": \"Doe\",         \"email\": \"john.doe@gmail.com\",         \"currentPassword\": \"password\",         \"newPassword\": \"newpassword\"    }"))
            .Produces<ApiResponse<UserDto>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion1_0);

        app.MapDelete(
                "/api/users/{id:guid}",
                async (IMediator mediator, Guid id) =>
                {
                    await mediator.Send(new Commands.DeleteUser.DeleteUserCommand { Id = id });

                    return Results.Ok(new ApiResponse<object>(true, "User deleted successfully", null));
                })
            .UseTreblle()
            .WithTags("Users")
            .RequireAuthorization()
            .WithMetadata(new SwaggerOperationAttribute("Delete a User by its Id", "\n    DELETE /Users/00000000-0000-0000-0000-000000000000"))
            .Produces<ApiResponse<object>>(StatusCodes.Status204NoContent, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion1_0);

        return app;
    }
}
