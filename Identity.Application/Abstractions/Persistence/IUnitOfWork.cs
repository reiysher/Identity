namespace Identity.Application.Abstractions.Persistence;

public interface IUnitOfWork : IScopedService
{
    IUserRepository Users { get; }

    IRoleRepository Roles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
