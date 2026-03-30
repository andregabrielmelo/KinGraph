using KinGraph.Core.Enumerations;

namespace KinGraph.Core.ValueObjects;

public sealed class BloodType : SmartEnum<BloodType>
{
    public BloodGroup BloodGroup { get; }
    public RhFactor RhFactor { get; }

    private BloodType(string name, int value, BloodGroup group, RhFactor rh)
        : base(name, value)
    {
        BloodGroup = group;
        RhFactor = rh;
    }

    public static readonly BloodType A_Positive = new("A+", 1, BloodGroup.A, RhFactor.Positive);
    public static readonly BloodType A_Negative = new("A-", 2, BloodGroup.A, RhFactor.Negative);
    public static readonly BloodType B_Positive = new("B+", 3, BloodGroup.B, RhFactor.Positive);
    public static readonly BloodType B_Negative = new("B-", 4, BloodGroup.B, RhFactor.Negative);
    public static readonly BloodType AB_Positive = new("AB+", 5, BloodGroup.AB, RhFactor.Positive);
    public static readonly BloodType AB_Negative = new("AB-", 6, BloodGroup.AB, RhFactor.Negative);
    public static readonly BloodType O_Positive = new("O+", 7, BloodGroup.O, RhFactor.Positive);
    public static readonly BloodType O_Negative = new("O-", 8, BloodGroup.O, RhFactor.Negative);

    public bool CanDonateTo(BloodType recipient)
    {
        // TODO: implement the standard blood compatibility rules here
        throw new NotImplementedException();
    }

    public override string ToString() => Name;
}
