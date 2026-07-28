namespace KinGraph.Core.ValueObjects;

public class ExtraField(string name, string value) : ValueObject
{
    public string Name { get; private set; } = name;
    public string Value { get; private set; } = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }

    public override string ToString() => Value;
}
