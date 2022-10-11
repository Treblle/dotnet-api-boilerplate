namespace DotNet_API_Boilerplate.Infrastructure.Databases.Blog.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DotNet_API_Boilerplate.Core.Posts.Entities;

internal class PostConfiguration : EntityConfiguration<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);

        builder.HasOne(m => m.User).WithMany(r => r.Posts).HasForeignKey(r => r.UserId);
    }
}
