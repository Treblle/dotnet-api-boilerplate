namespace DotNet_API_Boilerplate.Infrastructure;
using Databases.Blog;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;
using DotNet_API_Boilerplate.Core.Users.Entities;
using DotNet_API_Boilerplate.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApiDbContext>(ServiceLifetime.Singleton);

        services.AddIdentityCore<User>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApiDbContext>();
        services.AddTransient<IPostRepository, PostRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }

    public static void ConfigureInfrastructure(this IServiceProvider services)
    {
        var context = services.GetRequiredService<ApiDbContext>();
        if (context != null)
        {
            context.Database.Migrate();
            // Create an initial user for demo purposes. Remove if using in production.
            if (!context.Users.Any())
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var initialUser = new User
                {
                    Email = "john.doe@gmail.com",
                    UserName = "john.doe@gmail.com",
                    FirstName = "John",
                    LastName = "Doe"
                };
                userManager.CreateAsync(initialUser, "password").Wait();
            }
        }
    }
}
