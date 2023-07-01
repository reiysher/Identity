namespace Identity.Domain.Users;

public sealed class Email : ValueObject
{
    private const string _pattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";

    private string _value = default!;
    private bool _confirmed;

    public string Value => _value;

    public bool Confirmed => _confirmed;

    private Email() { }

    internal Email(string? value)
    {
        if (String.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, _pattern))
        {
            throw new ValidationException(Messages.Exceptions.InvalidEmail);
        }

        _value = value;
        _confirmed = false;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
