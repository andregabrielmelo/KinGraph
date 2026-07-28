namespace KinGraph.Core.Aggregates.PersonAggregate.Specifications;

public class PersonByIdSpecification : Specification<Person>
{
    public PersonByIdSpecification(PersonId personId) =>
        Query.Where(person => person.Id == personId);
}
