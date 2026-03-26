namespace KinGraph.Core.ValueObjects;

public class Citizenship(Country country, DateTime? acquiredAt) : ValueObject
{
    public Country Country { get; private set; } = country;
    public DateTime? AcquiredAt { get; private set; } = acquiredAt;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Country;
        yield return AcquiredAt;
    }

    public override string ToString()
    {
        return AcquiredAt is null
            ? Country.ToString()
            : $"{Country} (since {AcquiredAt:yyyy-MM-dd})";
    }
}
