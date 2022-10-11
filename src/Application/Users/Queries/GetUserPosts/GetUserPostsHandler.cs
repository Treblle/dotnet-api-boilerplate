namespace DotNet_API_Boilerplate.Core.Users.Queries.GetUserPosts;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;
using DotNet_API_Boilerplate.Core.Posts.Dto;

public class GetUserPostsHandler : IRequestHandler<GetUserPostsQuery, List<PostDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public GetUserPostsHandler(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }

    public async Task<List<PostDto>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
    {
        var result = _postRepository.GetList(cancellationToken).Where(x => x.UserId == request.Id.ToString());

        return result.ProjectTo<PostDto>(_mapper.ConfigurationProvider).ToList();
    }
}
