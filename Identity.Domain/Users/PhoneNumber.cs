namespace Identity.Domain.Users;

public sealed class PhoneNumber : ValueObject
{
    private const string _pattern = "^[0-9]+$";

    private string _value = default!;
    private bool _confirmed;


    public string Value => _value;

    public bool Confirmed => _confirmed;

    private PhoneNumber() { }

    internal PhoneNumber(string? phoneNumber)
    {
        if (String.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 10 || !Regex.IsMatch(phoneNumber, _pattern))
        {
            throw new ValidationException(Messages.Exceptions.InvalidPhoneNumber);
        }

        _value = phoneNumber;
        _confirmed = false;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
