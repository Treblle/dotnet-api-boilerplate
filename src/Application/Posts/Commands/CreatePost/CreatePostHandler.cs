namespace DotNet_API_Boilerplate.Core.Posts.Commands.CreatePost;

using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;
using DotNet_API_Boilerplate.Core.Common.Contracts.Services;
using DotNet_API_Boilerplate.Core.Common.Enums;
using DotNet_API_Boilerplate.Core.Posts.Dto;

public class CreatePostHandler : IRequestHandler<CreatePostCommand, PostDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public CreatePostHandler(IUserRepository userRepository, IPostRepository postRepository, IAuthService authService, IMapper mapper)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = Guid.Parse(_authService.GetCurrentUserIdentityId());
        if (!await _userRepository.UserExists(currentUserId, cancellationToken))
        {
            NotFoundException.Throw(EntityType.User);
        }
        var post = _postRepository.CreatePost(currentUserId, request.Title, request.Content, cancellationToken);
        return _mapper.Map<PostDto>(post);
    }
}
