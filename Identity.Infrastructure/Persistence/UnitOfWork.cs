using Identity.Application.Abstractions.Persistence;
using Identity.Application.Abstractions.Persistence.Repositories;
using Identity.Infrastructure.Persistence.Contexts;

namespace Identity.Infrastructure.Persistence;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    private readonly IUserRepository _users;
    private readonly IRoleRepository _roles;

    public UnitOfWork(ApplicationDbContext dbContext,
        IUserRepository users,
        IRoleRepository roles)
    {
        _dbContext = dbContext;
        _users = users;
        _roles = roles;
    }

    public IUserRepository Users => _users;

    public IRoleRepository Roles => _roles;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
