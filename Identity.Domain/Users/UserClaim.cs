namespace Identity.Domain.Users;

public sealed class UserClaim : Entity
{
    private string _id = default!;
    private string _userId = default!;
    private string _type = default!;
    private string _value = default!;

    public string Id => _id;

    public string UserId => _userId;

    public string Type => _type;

    public string Value => _value;

    public Claim ToClaim() =>
        new (Type, Value);

    public void InitializeFromClaim(Claim other)
    {
        _type = other.Type;
        _value = other.Value;
    }

    private UserClaim() { }

    public UserClaim(string userId, string type, string value)
    {
        _userId = userId;
        _type = type;
        _value = value;
    }
}
