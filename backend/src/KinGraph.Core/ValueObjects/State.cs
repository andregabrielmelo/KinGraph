namespace KinGraph.Core.ValueObjects;

public class State(string name) : ValueObject
{
    public string Name { get; private set; } = name;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

    public override string ToString() => Name;
}
