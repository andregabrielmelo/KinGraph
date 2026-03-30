using KinGraph.Core.Enumerations;

namespace KinGraph.Core.Aggregates.PersonAggregate;

public class FamilyRelationship : Relationship
{
    public override RelationshipType Type => RelationshipType.Family;

    public int GenerationOffset { get; }
    public int Degree { get; }
    public bool IsByMarriage { get; }
    public bool IsHalf { get; }
    public Gender? Gender { get; }

    private FamilyRelationship(
        PersonId relatedPersonId,
        int generationOffset,
        int degree,
        bool isByMarriage,
        bool isHalf,
        Gender? gender
    )
        : base(relatedPersonId)
    {
        GenerationOffset = generationOffset;
        Degree = degree;
        IsByMarriage = isByMarriage;
        IsHalf = isHalf;
        Gender = gender;
    }

    // Factory methods
    public static FamilyRelationship Parent(PersonId id, Gender gender) =>
        new(id, -1, 0, false, false, gender);

    public static FamilyRelationship Sibling(PersonId id, Gender gender) =>
        new(id, 0, 1, false, false, gender);

    public static FamilyRelationship Cousin(PersonId id, int degree) =>
        new(id, 0, degree, false, false, null);
}
