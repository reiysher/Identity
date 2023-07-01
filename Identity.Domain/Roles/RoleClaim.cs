namespace Identity.Domain.Roles;

public sealed class RoleClaim : Entity
{
    private string _id = default!;
    private string _roleId = default!;
    private string? _type;
    private string? _value;

    public string Id => _id;

    public string RoleId => _roleId;

    public string? Type => _type;

    public string? Value => _value;

    public Claim ToClaim() =>
        new (Type!, Value!);

    public void InitializeFromClaim(Claim? other)
    {
        _type = other?.Type;
        _value = other?.Value;
    }

    private RoleClaim() { }

    public RoleClaim(string roleId, string type, string value)
    {
        _roleId = roleId;
        _type = type;
        _value = value;
    }
}
