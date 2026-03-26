namespace KinGraph.Core.ValueObjects;

public class GovermentalDocument(
    string type,
    string number,
    DateTime? expirationDate,
    string? issuingCountry
) : ValueObject
{
    public string Type { get; private set; } = type; // TODO: e.g., Passport, ID
    public string Number { get; private set; } = number;
    public DateTime? ExpirationDate { get; private set; } = expirationDate;
    public string? IssuingCountry { get; private set; } = issuingCountry;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Type;
        yield return Number;
        yield return ExpirationDate;
        yield return IssuingCountry;
    }

    public override string ToString()
    {
        return $"{Type}: {Number}";
    }
}
