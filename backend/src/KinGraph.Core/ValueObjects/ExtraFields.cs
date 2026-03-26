namespace KinGraph.Core.ValueObjects;

public class ExtraFields(string name, string value) : ValueObject
{
    public string Name { get; } = name;
    public string Value { get; } = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }

    public override string ToString() => Value;
}
