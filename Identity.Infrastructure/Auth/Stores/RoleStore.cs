using Identity.Domain.Roles;
using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Auth.Stores;

internal class RoleStore : IRoleStore<Role>
{
    private readonly ApplicationDbContext _dbContext;

    public RoleStore(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Role?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Role>()
            .SingleOrDefaultAsync(r => r.Id == roleId, cancellationToken);
    }

    public Task<Role?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Role>()
            .SingleOrDefaultAsync(r => r.Name!.ToLower() == normalizedRoleName.ToLower(), cancellationToken);
    }

    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(role, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        _dbContext.Set<Role>().Update(role);
        return Task.FromResult(IdentityResult.Success);
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        _dbContext.Set<Role>().Remove(role);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<string?> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name?.ToUpper());
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Id);
    }

    public Task<string?> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name);
    }

    public Task SetNormalizedRoleNameAsync(Role role, string? normalizedName, CancellationToken cancellationToken)
    {
        role.SetName(normalizedName);
        return Task.CompletedTask;
    }

    public Task SetRoleNameAsync(Role role, string? roleName, CancellationToken cancellationToken)
    {
        role.SetName(roleName);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
