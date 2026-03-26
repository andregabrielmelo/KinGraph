namespace KinGraph.Core.ValueObjects;

public class DateOfBirth(DateTime value) : ValueObject
{
    public DateTime Value { get; set; } = value;

    public override string ToString() => Value.ToString("yyyy-MM-dd");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public int GetAge()
    {
        var today = DateTime.Today;
        var age = today.Year - Value.Year;
        if (Value.Date > today.AddYears(-age))
        {
            age--;
        }
        return age;
    }
}
