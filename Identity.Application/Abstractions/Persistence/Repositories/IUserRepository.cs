namespace Identity.Application.Abstractions.Persistence.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserById(string userId, CancellationToken cancellationToken);

    Task<User?> GetUserByEmail(string? email, CancellationToken cancellationToken);
}
