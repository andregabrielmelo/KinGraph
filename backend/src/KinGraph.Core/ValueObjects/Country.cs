using KinGraph.Core.Enumerations;

namespace KinGraph.Core.ValueObjects;

public sealed class Country : SmartEnum<Country>
{
    public Continent Continent { get; }
    public string Sovereignty { get; }
    public string Alpha2Code { get; }
    public string Alpha3Code { get; }
    public string NumericCode { get; }
    public string Tld { get; }

    private Country(
        string name,
        int value,
        Continent continent,
        string sovereignty,
        string alpha2Code,
        string alpha3Code,
        string numericCode,
        string tld
    )
        : base(name, value)
    {
        Continent = continent;
        Sovereignty = sovereignty;
        Alpha2Code = alpha2Code;
        Alpha3Code = alpha3Code;
        NumericCode = numericCode;
        Tld = tld;
    }

    // Examples (you would expand this list)
    public static readonly Country Brazil = new(
        nameof(Brazil),
        1,
        Continent.SouthAmerica,
        "Federative Republic of Brazil",
        "BR",
        "BRA",
        "076",
        ".br"
    );

    public static readonly Country UnitedStates = new(
        nameof(UnitedStates),
        2,
        Continent.NorthAmerica,
        "United States of America",
        "US",
        "USA",
        "840",
        ".us"
    );

    public static readonly Country Germany = new(
        nameof(Germany),
        3,
        Continent.Europe,
        "Federal Republic of Germany",
        "DE",
        "DEU",
        "276",
        ".de"
    );

    public static Country FromAlpha2(string code) =>
        List.FirstOrDefault(c => c.Alpha2Code == code.ToUpper())
        ?? throw new ArgumentException($"Invalid Alpha2 code: {code}");

    public static Country FromAlpha3(string code) =>
        List.FirstOrDefault(c => c.Alpha3Code == code.ToUpper())
        ?? throw new ArgumentException($"Invalid Alpha3 code: {code}");

    public static Country FromNumeric(string code) =>
        List.FirstOrDefault(c => c.NumericCode == code)
        ?? throw new ArgumentException($"Invalid numeric code: {code}");

    public override string ToString() => Name;
}
