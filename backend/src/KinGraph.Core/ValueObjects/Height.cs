namespace KinGraph.Core.ValueObjects;

public class Height(decimal value, LengthUnit unit) : ValueObject
{
    public decimal Value { get; private set; } = value;
    public LengthUnit Unit { get; private set; } = unit;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Unit;
    }

    public override string ToString()
    {
        return $"{Value} {Unit}";
    }
}
