using Identity.Application.Abstractions.Persistence.Repositories;
using Identity.Domain.Users;
using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetUserByEmail(string? email, CancellationToken cancellationToken)
    {
        if (String.IsNullOrWhiteSpace(email))
            return Task.FromResult<User?>(null);

        return _dbContext.Set<User>()
            .Include(user => user.Roles!)
            .SingleOrDefaultAsync(user => user.Email!.Value == email, cancellationToken);
    }

    public Task<User?> GetUserById(string userId, CancellationToken cancellationToken)
    {
        return _dbContext.Set<User>()
            .Include(user => user.Roles!)
            .SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }
}
