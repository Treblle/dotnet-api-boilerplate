namespace Treblle_Core_API_Boilerplate.Infrastructure.Databases.Blog.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Treblle_Core_API_Boilerplate.Core.Users.Entities;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

    }
}
