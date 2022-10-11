namespace DotNet_API_Boilerplate.Core.Users.Queries.GetUserPosts;
using MediatR;
using System.ComponentModel.DataAnnotations;
using DotNet_API_Boilerplate.Core.Posts.Dto;

public class GetUserPostsQuery : IRequest<List<PostDto>>
{
    [Required]
    public Guid Id { get; init; }
}
