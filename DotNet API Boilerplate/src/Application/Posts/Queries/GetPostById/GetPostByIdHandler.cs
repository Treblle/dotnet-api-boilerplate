namespace DotNet_API_Boilerplate.Core.Posts.Queries.GetPostById;
using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;
using DotNet_API_Boilerplate.Core.Common.Enums;
using DotNet_API_Boilerplate.Core.Common.Exceptions;
using DotNet_API_Boilerplate.Core.Posts.Dto;

public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostDto>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public GetPostByIdHandler(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = _repository.GetById(request.Id, cancellationToken);
            NotFoundException.ThrowIfNull(result, EntityType.Post);
            return _mapper.Map<PostDto>(result);
        }
        catch (InvalidOperationException ex)
        {
            NotFoundException.Throw(EntityType.Post);
            return null;
        }
    }
}
