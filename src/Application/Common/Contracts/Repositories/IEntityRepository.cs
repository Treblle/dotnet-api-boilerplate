namespace DotNet_API_Boilerplate.Core.Common.Contracts.Repositories;

using DotNet_API_Boilerplate.Core.Common.Entities;

public interface IEntityRepository<T> where T : Entity
{
    void Delete(Guid uuid, CancellationToken cancellationToken);
    IQueryable<T> GetList(CancellationToken cancellationToken);
    T GetById(Guid uuid, CancellationToken cancellationToken);
    bool EntityExists(Guid uuid, CancellationToken cancellationToken);
}
