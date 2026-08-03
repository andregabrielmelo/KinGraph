using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Aggregates.PersonAggregate.Specifications;
using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.Aggregates.UserAggregate.Specifications;
using KinGraph.Core.Enumerations;
using KinGraph.Core.ValueObjects;

namespace KinGraph.UseCases.Persons.UpdateOwnProfile;

public record UpdateOwnProfileCommand(
    UserId UserId,
    Gender? Gender,
    DateTime? DateOfBirth,
    string? OccupationName,
    decimal? OccupationSalary
) : ICommand<Result<PersonProfileDto>>;

public class UpdateOwnProfileHandler(
    IRepository<User> _userRepository,
    IRepository<Person> _personRepository
) : ICommandHandler<UpdateOwnProfileCommand, Result<PersonProfileDto>>
{
    public async ValueTask<Result<PersonProfileDto>> Handle(
        UpdateOwnProfileCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.FirstOrDefaultAsync(
            new UserByIdSpecification(command.UserId),
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

        if (command.Gender is not null)
        {
            person.UpdateGender(command.Gender.Value);
        }

        if (command.DateOfBirth is not null)
        {
            person.UpdateDateOfBirth(new DateOfBirth(command.DateOfBirth.Value));
        }

        if (command.OccupationName is not null)
        {
            person.UpdateOccupation(new Occupation(command.OccupationName, command.OccupationSalary ?? 0m));
        }

        await _personRepository.UpdateAsync(person, cancellationToken);

        return new PersonProfileDto(
            person.Name.Value,
            person.Gender,
            person.DateOfBirth?.Value,
            person.Occupation?.Name,
            person.Occupation?.Salary
        );
    }
}
