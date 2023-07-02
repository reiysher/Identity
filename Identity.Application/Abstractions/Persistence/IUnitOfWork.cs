namespace Identity.Application.Abstractions.Persistence;

public interface IUnitOfWork : IScopedService
{

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
