using Identity.Application.Abstractions.Persistence.Repositories;
using Identity.Infrastructure.Persistence.Contexts;

namespace Identity.Infrastructure.Persistence.Repositories;

internal class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
