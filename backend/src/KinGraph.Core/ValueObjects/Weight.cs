namespace KinGraph.Core.ValueObjects;

public class Weight(decimal value, WeightUnit unit) : ValueObject
{
    public decimal Value { get; private set; } = value;
    public WeightUnit Unit { get; private set; } = unit;

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
