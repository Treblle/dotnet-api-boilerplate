namespace Treblle_Core_API_Boilerplate.Core.Posts.Queries.GetPosts;
using MediatR;
using Treblle_Core_API_Boilerplate.Core.Posts.Dto;

public class GetPostsQuery : IRequest<List<PostDto>>
{
}
