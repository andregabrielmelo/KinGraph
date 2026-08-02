using KinGraph.Core.Enumerations;

namespace KinGraph.Core.Aggregates.PersonAggregate;

public class FamilyRelationship : Relationship
{
    public override RelationshipType Type => RelationshipType.Family;

    public int GenerationOffset { get; }
    public int Degree { get; }
    public bool IsByMarriage { get; }
    public bool IsHalf { get; }

    private FamilyRelationship(
        PersonId relatedPersonId,
        int generationOffset,
        int degree,
        bool isByMarriage,
        bool isHalf
    )
        : base(relatedPersonId)
    {
        GenerationOffset = generationOffset;
        Degree = degree;
        IsByMarriage = isByMarriage;
        IsHalf = isHalf;
    }

    // Factory methods
    public static FamilyRelationship Parent(
        PersonId id,
        bool isByMarriage = false,
        bool isHalf = false
    ) => new(id, -1, 0, isByMarriage, isHalf);

    public static FamilyRelationship Sibling(
        PersonId id,
        bool isByMarriage = false,
        bool isHalf = false
    ) => new(id, 0, 1, isByMarriage, isHalf);

    public static FamilyRelationship Cousin(
        PersonId id,
        int degree,
        bool isByMarriage = false,
        bool isHalf = false
    ) => new(id, 0, degree, isByMarriage, isHalf);

    public static FamilyRelationship Child(
        PersonId id,
        bool isByMarriage = false,
        bool isHalf = false
    ) => new(id, 1, 0, isByMarriage, isHalf);
}
