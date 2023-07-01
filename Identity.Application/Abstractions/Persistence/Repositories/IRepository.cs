namespace Identity.Application.Abstractions.Persistence.Repositories;

public interface IRepository<TEntity> : IScopedService
    where TEntity : class, IAggregateRoot
{
}
