namespace Treblle_Core_API_Boilerplate.Core.Users.Commands.DeleteUser;
using MediatR;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; init; }
}
