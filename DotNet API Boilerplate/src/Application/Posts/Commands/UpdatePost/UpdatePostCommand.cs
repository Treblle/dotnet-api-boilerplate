namespace Treblle_Core_API_Boilerplate.Core.Posts.Commands.UpdatePost;
using MediatR;
using Treblle_Core_API_Boilerplate.Core.Posts.Dto;

public class UpdatePostCommand : IRequest<PostDto>
{
    public Guid Id { get; set; }
    public string Title { get; init; }
    public string Content { get; init; }
}
