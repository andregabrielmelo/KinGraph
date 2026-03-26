namespace KinGraph.Core.ValueObjects;

public class Hobby(string name, string? description) : ValueObject
{
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Description;
    }

    public override string ToString()
    {
        return Name;
    }
}
