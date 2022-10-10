namespace Treblle_Core_API_Boilerplate.Infrastructure.Repositories;

using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Repositories;
using Treblle_Core_API_Boilerplate.Core.Posts.Entities;
using Treblle_Core_API_Boilerplate.Infrastructure.Databases.Blog;

internal class PostRepository : EntityRepository<Post>, IPostRepository
{
    private readonly ApiDbContext _context;
    public PostRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

    public override Post GetById(Guid uuid, CancellationToken cancellationToken)
    {
        return _context.Posts.Where(x => x.Uuid == uuid).Include(x => x.User).First();
    }

    public Post CreatePost(Guid userId, string title, string content, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Title = title,
            Content = content,
            UserId = userId.ToString()
        };
        _context.Posts.Add(post);
        _context.SaveChanges();
        return GetById(post.Uuid, cancellationToken);
    }

    public Post UpdatePost(Guid id, Guid userId, string title, string content, CancellationToken cancellationToken)
    {
        var existingPost = _context.Posts.First(x => x.Uuid == id);
        existingPost.UserId = userId.ToString();
        existingPost.Title = title;
        existingPost.Content = content;
        _context.Update(existingPost);
        _context.SaveChanges();
        return _context.Posts.First(x => x.Uuid == id);
    }
}
