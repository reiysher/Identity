namespace Identity.Domain.Users;

public sealed class User : Entity, IAggregateRoot
{
    private string _id = default!;
    private string? _userName;
    private Email? _email;
    private PhoneNumber? _phoneNumber;
    private string? _passwordHash;
    private readonly HashSet<UserRole>? _roles;
    private readonly HashSet<UserClaim>? _claims;

    public string Id => _id;

    public string? UserName => _userName;

    public Email? Email => _email;

    public string? PasswordHash => _passwordHash;

    public PhoneNumber? PhoneNumber => _phoneNumber;

    public IReadOnlyCollection<UserRole>? Roles => _roles?.ToList();

    public IReadOnlyCollection<UserClaim>? Claims => _claims?.ToList();

    private User() { }

    private User(string id, Email email, PhoneNumber phoneNumber, string? userName = null)
    {
        _id = id;
        _userName = userName;
        _email = email;
        _phoneNumber = phoneNumber;
        _roles = new HashSet<UserRole>();
        _claims = new HashSet<UserClaim>();
    }

    public static User Create(string email, string phoneNumber, string? userName = null)
    {
        var userId = Guid.NewGuid().ToString();
        var emailModel = new Email(email);
        var userPhoneNumber = new PhoneNumber(phoneNumber);

        return new User(userId, emailModel, userPhoneNumber, userName);
    }

    public void SetPasswordHash(string? passwordHash)
    {
        _passwordHash = passwordHash;
    }

    public void SetUserName(string? name)
    {
        _userName = name;
    }
}
