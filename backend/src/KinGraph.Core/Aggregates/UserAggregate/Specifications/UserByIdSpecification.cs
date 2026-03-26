namespace KinGraph.Core.Aggregates.UserAggregate.Specifications;

public class UserByIdSpecification : Specification<User>
{
    public UserByIdSpecification(UserId contributorId) =>
        Query.Where(contributor => contributor.Id == contributorId);
}
