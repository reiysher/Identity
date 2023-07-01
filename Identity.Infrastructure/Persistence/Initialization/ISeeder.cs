using Identity.Application.Abstractions.Common;
using Identity.Infrastructure.Persistence.Contexts;

namespace Identity.Infrastructure.Persistence.Initialization;

internal interface ISeeder : IScopedService
{
    int Order { get; }
    Task SeedAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken);
}
