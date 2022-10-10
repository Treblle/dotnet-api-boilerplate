namespace DotNet_API_Boilerplate.Core.Posts.Queries.GetPosts;
using MediatR;
using DotNet_API_Boilerplate.Core.Posts.Dto;

public class GetPostsQuery : IRequest<List<PostDto>>
{
}
