using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Aggregates.PersonAggregate.Specifications;
using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.Aggregates.UserAggregate.Specifications;

namespace KinGraph.UseCases.Persons.GetOwnProfile;

public record GetOwnProfileQuery(UserId UserId) : IQuery<Result<PersonProfileDto>>;

public class GetOwnProfileHandler(
    IRepository<User> _userRepository,
    IRepository<Person> _personRepository
) : IQueryHandler<GetOwnProfileQuery, Result<PersonProfileDto>>
{
    public async ValueTask<Result<PersonProfileDto>> Handle(
        GetOwnProfileQuery query,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.FirstOrDefaultAsync(
            new UserByIdSpecification(query.UserId),
            cancellationToken
        );
        if (user is null)
            return Result.NotFound();

        var person = await _personRepository.FirstOrDefaultAsync(
            new PersonByIdSpecification(user.PersonId),
            cancellationToken
        );
        if (person is null)
            return Result.NotFound();

        return new PersonProfileDto(
            person.Name.Value,
            person.Gender,
            person.DateOfBirth?.Value,
            person.Occupation?.Name,
            person.Occupation?.Salary
        );
    }
}
