namespace KinGraph.Core.ValueObjects;

public sealed class LengthUnit : SmartEnum<LengthUnit>
{
    public string Abbreviation { get; }

    private LengthUnit(string name, int value, string abbr)
        : base(name, value)
    {
        Abbreviation = abbr;
    }

    public static readonly LengthUnit Centimeter = new(nameof(Centimeter), 1, "cm");
    public static readonly LengthUnit Meter = new(nameof(Meter), 2, "m");
    public static readonly LengthUnit Foot = new(nameof(Foot), 3, "ft");

    public decimal ConvertTo(decimal value, LengthUnit target)
    {
        if (this == target)
            return value;

        if (this == Meter && target == Centimeter)
            return value * 100;

        if (this == Centimeter && target == Meter)
            return value / 100;

        if (this == Meter && target == Foot)
            return value * 3.28084m;

        if (this == Foot && target == Meter)
            return value / 3.28084m;

        throw new NotSupportedException($"Conversion not supported: {Name} → {target.Name}");
    }
}
