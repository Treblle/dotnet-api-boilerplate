namespace Treblle_Core_API_Boilerplate.Core.Users.Commands.CreateUser;

using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Treblle_Core_API_Boilerplate.Core.Users.Entities;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Repositories;
using Treblle_Core_API_Boilerplate.Core.Users.Dto;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email
        };
        var createdUser = await _userRepository.RegisterUserAsync(user, request.Password, cancellationToken);
        return _mapper.Map<UserDto>(createdUser);
    }
}
