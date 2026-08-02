using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Aggregates.PersonAggregate.Specifications;
using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.Aggregates.UserAggregate.Specifications;
using KinGraph.Core.Enumerations;

namespace KinGraph.UseCases.Persons.Relationships.Create;

public enum FamilyRelationshipKind
{
    Parent,
    Sibling,
    Cousin,
}

public record CreateRelationshipCommand(
    UserId SourceUserId,
    UserId RelatedUserId,
    RelationshipType Type,
    FamilyRelationshipKind? Kind,
    int? CousinDegree,
    bool IsByMarriage,
    bool IsHalf
) : ICommand<Result>;

public class CreateRelationshipHandler(
    IRepository<User> _userRepository,
    IRepository<Person> _personRepository
) : ICommandHandler<CreateRelationshipCommand, Result>
{
    public async ValueTask<Result> Handle(
        CreateRelationshipCommand command,
        CancellationToken cancellationToken
    )
    {
        var sourceUser = await _userRepository.FirstOrDefaultAsync(
            new UserByIdSpecification(command.SourceUserId),
            cancellationToken
        );
        var relatedUser = await _userRepository.FirstOrDefaultAsync(
            new UserByIdSpecification(command.RelatedUserId),
            cancellationToken
        );
        if (sourceUser is null || relatedUser is null)
            return Result.NotFound();

        if (sourceUser.PersonId == relatedUser.PersonId)
            return Result.Invalid(
                new ValidationError("A person cannot have a relationship with themselves.")
            );

        var sourcePerson = await _personRepository.FirstOrDefaultAsync(
            new PersonByIdSpecification(sourceUser.PersonId),
            cancellationToken
        );
        var relatedPerson = await _personRepository.FirstOrDefaultAsync(
            new PersonByIdSpecification(relatedUser.PersonId),
            cancellationToken
        );
        if (sourcePerson is null || relatedPerson is null)
            return Result.NotFound();

        Relationship forward;
        Relationship reciprocal;

        if (command.Type == RelationshipType.Friend)
        {
            forward = FriendRelationship.Create(relatedPerson.Id);
            reciprocal = FriendRelationship.Create(sourcePerson.Id);
        }
        else
        {
            switch (command.Kind)
            {
                case FamilyRelationshipKind.Parent:
                    if (sourcePerson.Gender is null)
                        return Result.Invalid(
                            new ValidationError(
                                "Source person must have a Gender set on their profile first."
                            )
                        );
                    forward = FamilyRelationship.Parent(
                        relatedPerson.Id,
                        command.IsByMarriage,
                        command.IsHalf
                    );
                    reciprocal = FamilyRelationship.Child(
                        sourcePerson.Id,
                        command.IsByMarriage,
                        command.IsHalf
                    );
                    break;
                case FamilyRelationshipKind.Sibling:
                    if (sourcePerson.Gender is null)
                        return Result.Invalid(
                            new ValidationError(
                                "Source person must have a Gender set on their profile first."
                            )
                        );
                    forward = FamilyRelationship.Sibling(
                        relatedPerson.Id,
                        command.IsByMarriage,
                        command.IsHalf
                    );
                    reciprocal = FamilyRelationship.Sibling(
                        sourcePerson.Id,
                        command.IsByMarriage,
                        command.IsHalf
                    );
                    break;
                case FamilyRelationshipKind.Cousin:
                    if (command.CousinDegree is null or < 2)
                        return Result.Invalid(new ValidationError("CousinDegree must be >= 2."));
                    forward = FamilyRelationship.Cousin(
                        relatedPerson.Id,
                        command.CousinDegree.Value,
                        command.IsByMarriage,
                        command.IsHalf
                    );
                    reciprocal = FamilyRelationship.Cousin(
                        sourcePerson.Id,
                        command.CousinDegree.Value,
                        command.IsByMarriage,
                        command.IsHalf
                    );
                    break;
                default:
                    return Result.Invalid(
                        new ValidationError("Kind is required for a Family relationship.")
                    );
            }
        }

        sourcePerson.AddRelationship(forward);
        relatedPerson.AddRelationship(reciprocal);

        await _personRepository.UpdateAsync(sourcePerson, cancellationToken);
        await _personRepository.UpdateAsync(relatedPerson, cancellationToken);

        return Result.Success();
    }
}
