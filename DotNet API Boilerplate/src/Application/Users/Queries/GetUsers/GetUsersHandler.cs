namespace Treblle_Core_API_Boilerplate.Core.Users.Queries.GetUsers;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Repositories;
using Treblle_Core_API_Boilerplate.Core.Users.Dto;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUsersHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetUsers(cancellationToken).ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToList();
    }
}
