namespace Identity.Domain.Roles;

public sealed class Role : Entity, IAggregateRoot
{
    private string _id = default!;
    private string? _name;
    private string? _description;
    private readonly HashSet<UserRole>? _users;
    private readonly HashSet<RoleClaim>? _claims;

    public string Id => _id;

    public string? Name => _name;

    public string? Description => _description;

    public IReadOnlyCollection<UserRole>? Users => _users?.ToList();

    public IReadOnlyCollection<RoleClaim>? Claims => _claims?.ToList();

    private Role() { }

    private Role(string name, string? description = null)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(Messages.Exceptions.InvalidRoleName);
        }

        _id = Guid.NewGuid().ToString();
        _name = name;
        _description = description;
    }

    public static Role Create(string name, string? description = null)
    {
        return new Role(name, description);
    }

    public void SetName(string? name)
    {
        _name = name;
    }
}
