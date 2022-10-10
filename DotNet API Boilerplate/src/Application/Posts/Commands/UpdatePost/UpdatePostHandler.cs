namespace Treblle_Core_API_Boilerplate.Core.Posts.Commands.UpdatePost;

using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Repositories;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Services;
using Treblle_Core_API_Boilerplate.Core.Common.Enums;
using Treblle_Core_API_Boilerplate.Core.Posts.Dto;

public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, PostDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public UpdatePostHandler(IUserRepository userRepository, IPostRepository postRepository, IAuthService authService, IMapper mapper)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        if (!_postRepository.EntityExists(request.Id, cancellationToken))
        {
            NotFoundException.Throw(EntityType.Post);
        }
        var currentUserId = Guid.Parse(_authService.GetCurrentUserIdentityId());
        if (!await _userRepository.UserExists(currentUserId, cancellationToken))
        {
            NotFoundException.Throw(EntityType.User);
        }
        var result = _postRepository.UpdatePost(request.Id, currentUserId, request.Title, request.Content, cancellationToken);
        return _mapper.Map<PostDto>(result);
    }
}
