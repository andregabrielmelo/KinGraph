namespace KinGraph.Core.ValueObjects;

public class Address(string street, City city, State state, string postalCode, Country country)
    : ValueObject
{
    public string Street { get; private set; } = street;
    public City City { get; private set; } = city;
    public State State { get; private set; } = state;
    public string PostalCode { get; private set; } = postalCode;
    public Country Country { get; private set; } = country;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return PostalCode;
        yield return Country;
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {State} {PostalCode}, {Country}";
    }
}
