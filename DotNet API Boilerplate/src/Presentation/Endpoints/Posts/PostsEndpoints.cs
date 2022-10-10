namespace DotNet_API_Boilerplate.Presentation.Endpoints.Posts;
using Errors;
using MediatR;
using Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using Commands = Core.Posts.Commands;
using Queries = Core.Posts.Queries;
using Treblle.Net.Core;
using Asp.Versioning.Builder;
using DotNet_API_Boilerplate.Presentation.Versioning;
using DotNet_API_Boilerplate.Core.Common;
using DotNet_API_Boilerplate.Core.Posts.Dto;

[ExcludeFromCodeCoverage]
public static class PostsEndpoints
{
    public static WebApplication MapPostEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGet(
                "/api/posts",
                async (IMediator mediator) =>
                {
                    var result = await mediator.Send(new Queries.GetPosts.GetPostsQuery());
                    return Results.Ok(new ApiResponse<List<PostDto>>(true, string.Empty, result));
                })
            .UseTreblle()
            .RequireAuthorization()
            .WithTags("Posts")
            .WithMetadata(new SwaggerOperationAttribute("Lookup all Posts", "\n    GET /Posts"))
            .Produces<ApiResponse<List<PostDto>>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion1_0);

        app.MapGet(
                "/api/posts/{id:guid}",
                async (IMediator mediator, Guid id) =>
                {
                    var result = await mediator.Send(new Queries.GetPostById.GetPostByIdQuery { Id = id });
                    return Results.Ok(new ApiResponse<PostDto>(true, string.Empty, result));
                })
            .UseTreblle()
            .RequireAuthorization()
            .WithTags("Posts")
            .WithMetadata(new SwaggerOperationAttribute("Lookup a Post by its Id", "\n    GET /Posts/00000000-0000-0000-0000-000000000000"))
            .Produces<ApiResponse<PostDto>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion1_0);

        app.MapPost(
                "/api/posts",
                async (IMediator mediator, HttpRequest httpRequest, CreatePostRequest request) =>
                {
                    var command = new Commands.CreatePost.CreatePostCommand()
                    {
                        Title = request.Title,
                        Content = request.Content
                    };
                    var result = await mediator.Send(command);
                    return Results.Created(UriHelper.GetEncodedUrl(httpRequest), new ApiResponse<PostDto>(true, "Post created successfully", result));
                })
            .UseTreblle()
            .WithTags("Posts")
            .RequireAuthorization()
            .WithMetadata(new SwaggerOperationAttribute("Create a Post", "\n    POST /Posts\n     {         \"title\": \"Post title\",         \"content\": \"This is a new post\"    }"))
            .Produces<ApiResponse<PostDto>>(StatusCodes.Status201Created, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion1_0);

        app.MapDelete(
                "/api/posts/{id:guid}",
                async (IMediator mediator, Guid id) =>
                {
                    await mediator.Send(new Commands.DeletePost.DeletePostCommand { Id = id });

                    return Results.Ok(new ApiResponse<object>(true, "Post deleted successfully", null));
                })
            .UseTreblle()
            .WithTags("Posts")
            .RequireAuthorization()
            .WithMetadata(new SwaggerOperationAttribute("Delete a Post by its Id", "\n    DELETE /Posts/00000000-0000-0000-0000-000000000000"))
            .Produces<ApiResponse<object>>(StatusCodes.Status204NoContent, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion1_0);

        app.MapPut(
                "/api/posts/{id:guid}",
                async (IMediator mediator, Guid id, UpdatePostRequest request) =>
                {
                    var command = new Commands.UpdatePost.UpdatePostCommand
                    {
                        Id = id,
                        Title = request.Title,
                        Content = request.Content
                    };

                    var result = await mediator.Send(command);

                    return Results.Ok(new ApiResponse<PostDto>(true, "Post updated successfully", result));
                })
            .UseTreblle()
            .WithTags("Posts")
            .RequireAuthorization()
            .WithMetadata(new SwaggerOperationAttribute("Update a Post", "\n    PUT /Posts/00000000-0000-0000-0000-000000000000\n      {         \"title\": \"Post title\",         \"content\": \"This is a new post\"   }"))
            .Produces<ApiResponse<PostDto>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiResponse<ApiError>>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion1_0);


        return app;
    }
}
