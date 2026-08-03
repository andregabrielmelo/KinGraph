using System.Text.RegularExpressions;

namespace KinGraph.Core.ValueObjects;

// https://www.w3.org/Protocols/rfc822/#z8
public class EmailAddress : ValueObject
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled
    );
    public string Value { get; private set; }
    public static EmailAddress Unknown { get; } = new EmailAddress(String.Empty);

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty");

        if (!EmailRegex.IsMatch(value))
            throw new ArgumentException("Invalid email format");

        Value = value;
    }

    public override string ToString()
    {
        if (this == Unknown)
            return "Unknown";

        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
