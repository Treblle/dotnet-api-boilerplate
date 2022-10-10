namespace DotNet_API_Boilerplate.Core.Posts.Commands.DeletePost;
using Common.Enums;
using Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;

public class DeletePostHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IPostRepository _repository;

    public DeletePostHandler(IPostRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        if (!_repository.EntityExists(request.Id, cancellationToken))
        {
            NotFoundException.Throw(EntityType.Post);
        }

        try
        {
            _repository.Delete(request.Id, cancellationToken);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            return Task.FromResult(false);
        }
    }
}
