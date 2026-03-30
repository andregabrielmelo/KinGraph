using KinGraph.Core.Enumerations;

namespace KinGraph.Core.Aggregates.PersonAggregate;

public class FriendRelationship : Relationship
{
    public override RelationshipType Type => RelationshipType.Friend;

    public static FriendRelationship Create(PersonId relatedPersonId) =>
        new FriendRelationship(relatedPersonId);

    private FriendRelationship(PersonId relatedPersonId)
        : base(relatedPersonId) { }
}
