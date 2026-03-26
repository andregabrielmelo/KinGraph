namespace KinGraph.Core.ValueObjects;

public class Profession(string name, decimal salary) : ValueObject
{
    public string Name { get; set; } = name;
    public decimal Salary { get; set; } = salary;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Salary;
    }

    public override string ToString() => $"{Name} with salary {Salary}";
}
