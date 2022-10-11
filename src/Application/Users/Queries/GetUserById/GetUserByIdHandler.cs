namespace DotNet_API_Boilerplate.Core.Users.Queries.GetUserById;

using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;
using DotNet_API_Boilerplate.Core.Users.Dto;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetUserById(request.Id, cancellationToken);

        NotFoundException.ThrowIfNull(result, EntityType.User);

        return _mapper.Map<UserDto>(result);
    }
}
