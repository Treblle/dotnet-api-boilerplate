namespace DotNet_API_Boilerplate.Core.Posts.Commands.DeletePost;
using MediatR;

public class DeletePostCommand : IRequest<bool>
{
    public Guid Id { get; init; }
}
