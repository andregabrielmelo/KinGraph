namespace KinGraph.Core.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public City City { get; private set; }
    public State State { get; private set; }
    public string PostalCode { get; private set; }
    public Country Country { get; private set; }

    private Address() { } // TODO: Made to work wirh EF core in entity configurations, consider a better way to handle this

    public Address(string street, City city, State state, string postalCode, Country country)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

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
