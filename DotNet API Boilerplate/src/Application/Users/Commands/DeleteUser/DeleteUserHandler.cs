namespace Treblle_Core_API_Boilerplate.Core.Users.Commands.DeleteUser;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Repositories;
using Treblle_Core_API_Boilerplate.Core.Common.Enums;
using Treblle_Core_API_Boilerplate.Core.Common.Exceptions;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.UserExists(request.Id, cancellationToken))
        {
            NotFoundException.Throw(EntityType.User);
        }

        await _userRepository.DeleteUserAsync(request.Id, cancellationToken);
        return true;
    }
}
