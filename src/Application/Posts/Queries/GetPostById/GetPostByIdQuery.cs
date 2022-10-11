namespace DotNet_API_Boilerplate.Core.Posts.Queries.GetPostById;
using MediatR;
using DotNet_API_Boilerplate.Core.Posts.Dto;

public class GetPostByIdQuery : IRequest<PostDto>
{
    public Guid Id { get; init; }
}
