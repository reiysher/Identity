namespace Identity.Application.Abstractions.Persistence;

public interface IUnitOfWork : IScopedService
{
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
