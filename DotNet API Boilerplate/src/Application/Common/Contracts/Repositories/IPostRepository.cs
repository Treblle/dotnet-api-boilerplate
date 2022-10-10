namespace DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;

using DotNet_API_Boilerplate.Core.Posts.Entities;

public interface IPostRepository : IEntityRepository<Post>
{
    Post CreatePost(Guid userId, string title, string content, CancellationToken cancellationToken);
    Post UpdatePost(Guid id, Guid userId, string title, string content, CancellationToken cancellationToken);
}
