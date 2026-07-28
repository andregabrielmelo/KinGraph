using KinGraph.Core.Enumerations;

namespace KinGraph.Core.ValueObjects;

public class GovermentalDocument(
    GovernmentalDocumentType type,
    string number,
    DateTime? expirationDate,
    Country? issuingCountry
) : ValueObject
{
    public GovernmentalDocumentType Type { get; private set; } = type;
    public string Number { get; private set; } = number; // TODO: Interisting to limit the length of the number based on the type of document, e.g. passport number is usually 9 characters long
    public DateTime? ExpirationDate { get; private set; } = expirationDate;
    public Country? IssuingCountry { get; private set; } = issuingCountry;

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
