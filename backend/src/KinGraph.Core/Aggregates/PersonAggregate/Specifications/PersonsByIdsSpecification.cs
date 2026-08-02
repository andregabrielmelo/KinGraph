namespace KinGraph.Core.Aggregates.PersonAggregate.Specifications;

public class PersonsByIdsSpecification : Specification<Person>
{
    public PersonsByIdsSpecification(IEnumerable<PersonId> personIds) =>
        Query.Where(person => personIds.Contains(person.Id));
}
