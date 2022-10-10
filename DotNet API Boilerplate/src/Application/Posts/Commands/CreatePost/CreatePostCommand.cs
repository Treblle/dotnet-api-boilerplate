namespace DotNet_API_Boilerplate.Core.Posts.Commands.CreatePost;
using MediatR;
using DotNet_API_Boilerplate.Core.Posts.Dto;

public class CreatePostCommand : IRequest<PostDto>
{
    public string Title { get; init; }
    public string Content { get; init; }
}
