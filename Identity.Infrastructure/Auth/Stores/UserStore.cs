using Identity.Domain.Users;
using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Auth.Stores;

internal class UserStore : IUserStore<User>
{
    private readonly ApplicationDbContext _dbContext;

    public UserStore(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return _dbContext.Set<User>()
            .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return _dbContext.Set<User>()
            .SingleOrDefaultAsync(u => u.UserName!.ToLower() == normalizedUserName.ToLower(), cancellationToken);
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Set<User>().Update(user);
        return Task.FromResult(IdentityResult.Success);
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Set<User>().Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName?.ToUpper());
    }

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id);
    }

    public Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetNormalizedUserNameAsync(User user, string? normalizedName, CancellationToken cancellationToken)
    {
        user.SetUserName(normalizedName);
        return Task.CompletedTask;
    }

    public Task SetUserNameAsync(User user, string? userName, CancellationToken cancellationToken)
    {
        user.SetUserName(userName);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
