namespace KinGraph.Core.Aggregates.PersonAggregate.Specifications;

public class PersonByIdWithRelationshipsSpecification : Specification<Person>
{
    public PersonByIdWithRelationshipsSpecification(PersonId personId) =>
        Query.Where(person => person.Id == personId).Include(person => person.Relationships);
}
