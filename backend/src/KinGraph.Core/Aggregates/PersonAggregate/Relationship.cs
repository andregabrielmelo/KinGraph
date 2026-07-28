using KinGraph.Core.Enumerations;

namespace KinGraph.Core.Aggregates.PersonAggregate;

public abstract class Relationship(PersonId relatedPersonId) : EntityBase
{
    public PersonId RelatedPersonId { get; protected set; } = relatedPersonId;

    public abstract RelationshipType Type { get; }
}
