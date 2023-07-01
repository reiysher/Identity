namespace Identity.Domain.Users;

public sealed class UserRole
{
    private string _userId = default!;
    private string _roleId = default!;

    public string UserId => _userId;

    public string RoleId => _roleId;

    private UserRole() { }

    public UserRole(string userId, string roleId)
    {
        _userId = userId;
        _roleId = roleId;
    }
}
