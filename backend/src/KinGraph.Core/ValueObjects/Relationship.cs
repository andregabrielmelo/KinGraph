using KinGraph.Core.Enumerations;
using KinGraph.Core.PersonAggregate;

namespace KinGraph.Core.ValueObjects;

public class Relationship : ValueObject
{
    public PersonId PersonId { get; private set; }
    public RelationshipType RelationshipType { get; private set; }

    public Relationship(PersonId personId, RelationshipType relationshipType)
    {
        PersonId = personId;
        RelationshipType = relationshipType;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PersonId;
        yield return RelationshipType;
    }

    public override string ToString()
    {
        return $"{RelationshipType} relationship with Person {PersonId.Value}";
    }
}
