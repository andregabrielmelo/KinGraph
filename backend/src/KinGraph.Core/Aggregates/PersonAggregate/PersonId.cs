namespace KinGraph.Core.Aggregates.PersonAggregate;

[ValueObject<int>]
public readonly partial struct PersonId
{
    private static Validation Validate(int value) =>
        value > 0 ? Validation.Ok : Validation.Invalid("PersonId must be positive.");
}
