using Ardalis.SmartEnum;

namespace KinGraph.Core.ValueObjects;

public sealed class WeightUnit : SmartEnum<WeightUnit>
{
    public string Abbreviation { get; }

    private WeightUnit(string name, int value, string abbr)
        : base(name, value)
    {
        Abbreviation = abbr;
    }

    public static readonly WeightUnit Kilogram = new(nameof(Kilogram), 1, "kg");
    public static readonly WeightUnit Pound = new(nameof(Pound), 2, "lb");

    public decimal ConvertTo(decimal value, WeightUnit target)
    {
        if (this == target)
            return value;

        if (this == Kilogram && target == Pound)
            return value * 2.20462m;

        if (this == Pound && target == Kilogram)
            return value / 2.20462m;

        throw new NotSupportedException($"Conversion not supported: {Name} → {target.Name}");
    }
}
