namespace Treblle_Core_API_Boilerplate.Core.Common.Contracts.Repositories;
using System.Threading.Tasks;
using Treblle_Core_API_Boilerplate.Core.Users.Entities;

public interface IUserRepository
{
    IQueryable<User> GetUsers(CancellationToken cancellationToken);
    Task<User> GetUserById(Guid id, CancellationToken cancellationToken);
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<bool> UserExists(Guid id, CancellationToken cancellationToken);
    Task<bool> UserExists(string email, CancellationToken cancellationToken);
    Task<IList<string>> GetUserRolesAsync(User user);
    Task<User> RegisterUserAsync(User userForRegistration, string password, CancellationToken cancellationToken);
    Task<User> UpdateUserAsync(Guid id, string email, string currentPassword, string newPassword, string firstName, string lastName, CancellationToken cancellationToken);
    Task<bool> ValidateUserAsync(string username, string password, CancellationToken cancellationToken);
    Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
}
