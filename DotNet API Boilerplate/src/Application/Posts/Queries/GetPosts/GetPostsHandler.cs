namespace Treblle_Core_API_Boilerplate.Core.Posts.Queries.GetPosts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Repositories;
using Treblle_Core_API_Boilerplate.Core.Posts.Dto;

public class GetPostsHandler : IRequestHandler<GetPostsQuery, List<PostDto>>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public GetPostsHandler(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetList(cancellationToken).ProjectTo<PostDto>(_mapper.ConfigurationProvider).ToList();
    }
}
