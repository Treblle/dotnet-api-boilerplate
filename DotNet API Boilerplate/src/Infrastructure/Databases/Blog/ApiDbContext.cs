namespace DotNet_API_Boilerplate.Infrastructure.Databases.Blog;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using DotNet_API_Boilerplate.Core.Common.Entities;
using DotNet_API_Boilerplate.Core.Posts.Entities;
using DotNet_API_Boilerplate.Core.Users.Entities;

internal class ApiDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _configuration;
    public ApiDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // connect to sql server with connection string from app settings
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("WebApiDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        OnBeforeSaveChanges();
        var saveResult = base.SaveChanges();
        OnAfterSaveChanges();
        return saveResult;
    }

    private void OnBeforeSaveChanges()
    {
        ChangeTracker.DetectChanges();
        var entries = ChangeTracker.Entries().Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((Entity)entry.Entity).DateCreated = DateTime.UtcNow;
                ((Entity)entry.Entity).Uuid = Guid.NewGuid();
            }
            ((Entity)entry.Entity).DateModified = DateTime.UtcNow;
        }
    }

    private void OnAfterSaveChanges()
    {
        // Executes after saving changes to the database
    }
}
