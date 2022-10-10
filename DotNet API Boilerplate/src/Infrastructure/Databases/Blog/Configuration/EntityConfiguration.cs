namespace Treblle_Core_API_Boilerplate.Infrastructure.Databases.Blog.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Treblle_Core_API_Boilerplate.Core.Common.Entities;

internal abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasAlternateKey(e => e.Uuid);
        builder.Property(m => m.Uuid).ValueGeneratedOnAdd().IsRequired();
        builder.Property(m => m.DateCreated).ValueGeneratedOnAdd().IsRequired();
        builder.Property(m => m.DateModified).ValueGeneratedOnAdd().IsRequired();
    }
}
