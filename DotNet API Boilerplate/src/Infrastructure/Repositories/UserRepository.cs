namespace DotNet_API_Boilerplate.Infrastructure.Repositories;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;
using DotNet_API_Boilerplate.Core.Common.Exceptions;
using DotNet_API_Boilerplate.Core.Users.Entities;
using DotNet_API_Boilerplate.Infrastructure.Databases.Blog;

internal class UserRepository : IUserRepository
{
    private readonly ApiDbContext _context;
    private readonly UserManager<User> _userManager;
    public UserRepository(ApiDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IQueryable<User> GetUsers(CancellationToken cancellationToken)
    {
        return _context.Set<User>().AsNoTracking();
    }

    public Task<User> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        return _userManager.FindByIdAsync(id.ToString());
    }
    public Task<User> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return _userManager.FindByEmailAsync(email);
    }

    public Task<User> GetUserByUsername(string username)
    {
        return _userManager.FindByNameAsync(username);
    }

    public async Task<bool> UserExists(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return user != null;
    }

    public async Task<bool> UserExists(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }

    public Task<IList<string>> GetUserRolesAsync(User user)
    {
        return _userManager.GetRolesAsync(user);
    }

    public async Task<User> RegisterUserAsync(User userForRegistration, string password, CancellationToken cancellationToken)
    {
        var result = await _userManager.CreateAsync(userForRegistration, password);
        UserCreateException.ThrowIfFailed(result);
        return await GetUserByEmail(userForRegistration.Email, cancellationToken);
    }
    public async Task<User> UpdateUserAsync(Guid id, string email, string currentPassword, string newPassword, string firstName, string lastName, CancellationToken cancellationToken)
    {
        var existingUser = await GetUserById(id, cancellationToken);
        existingUser.Email = email;
        existingUser.UserName = email;
        existingUser.FirstName = firstName;
        existingUser.LastName = lastName;
        UserUpdateException.ThrowIfFailed(await _userManager.ChangePasswordAsync(existingUser, currentPassword, newPassword));
        UserUpdateException.ThrowIfFailed(await _userManager.UpdateAsync(existingUser));
        return existingUser;
    }

    public async Task<bool> ValidateUserAsync(string username, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(username);
        var result = user != null && await _userManager.CheckPasswordAsync(user, password);
        return result;
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingUser = await GetUserById(id, cancellationToken);
        UserDeleteException.ThrowIfFailed(await _userManager.DeleteAsync(existingUser));
    }
}
