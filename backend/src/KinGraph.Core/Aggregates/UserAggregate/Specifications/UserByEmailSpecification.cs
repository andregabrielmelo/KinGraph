using KinGraph.Core.ValueObjects;

namespace KinGraph.Core.Aggregates.UserAggregate.Specifications;

public class UserByEmailSpecification : Specification<User>
{
    public UserByEmailSpecification(EmailAddress email) => Query.Where(user => user.Email == email);
}
