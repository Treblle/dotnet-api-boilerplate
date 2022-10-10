namespace Treblle_Core_API_Boilerplate.Infrastructure.Repositories;

using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Treblle_Core_API_Boilerplate.Core.Common.Contracts.Repositories;
using Treblle_Core_API_Boilerplate.Core.Common.Entities;
using Treblle_Core_API_Boilerplate.Infrastructure.Databases.Blog;

internal class EntityRepository<T> : IEntityRepository<T> where T : Entity
{
    private readonly ApiDbContext _context;

    public EntityRepository(ApiDbContext context)
    {
        _context = context;
    }
    public virtual T GetById(Guid uuid, CancellationToken cancellationToken)
    {
        return _context.Set<T>().First(x => x.Uuid == uuid);
    }

    public IQueryable<T> GetList(CancellationToken cancellationToken)
    {
        return _context.Set<T>().AsNoTracking();
    }
    public void Delete(Guid uuid, CancellationToken cancellationToken)
    {
        var entity = GetById(uuid, cancellationToken);
        if (entity != null)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }
    }

    public bool EntityExists(Guid uuid, CancellationToken cancellationToken)
    {
        return _context.Set<T>().Any(x => x.Uuid == uuid);
    }
}
