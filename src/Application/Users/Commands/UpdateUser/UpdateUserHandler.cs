namespace DotNet_API_Boilerplate.Core.Users.Commands.UpdateUser;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;
using DotNet_API_Boilerplate.Core.Common.Enums;
using DotNet_API_Boilerplate.Core.Common.Exceptions;
using DotNet_API_Boilerplate.Core.Users.Dto;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.UserExists(request.Id, cancellationToken))
        {
            NotFoundException.Throw(EntityType.User);
        }
        var updatedUser = await _userRepository
            .UpdateUserAsync(request.Id, request.Email, request.CurrentPassword, request.NewPassword, request.FirstName, request.LastName, cancellationToken);
        return _mapper.Map<UserDto>(updatedUser);
    }
}
