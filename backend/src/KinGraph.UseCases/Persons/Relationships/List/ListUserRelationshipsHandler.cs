
using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Aggregates.PersonAggregate.Specifications;
using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.Aggregates.UserAggregate.Specifications;

namespace KinGraph.UseCases.Persons.Relationships.List;

public record ListUserRelationshipsQuery(UserId UserId) : IQuery<Result<IEnumerable<RelationshipDto>>>;

public class ListUserRelationshipsHandler(
    IRepository<User> _userRepository,
    IRepository<Person> _personRepository
) : IQueryHandler<ListUserRelationshipsQuery, Result<IEnumerable<RelationshipDto>>>
{
    public async ValueTask<Result<IEnumerable<RelationshipDto>>> Handle(
        ListUserRelationshipsQuery query,
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
            new PersonByIdWithRelationshipsSpecification(user.PersonId),
            cancellationToken
        );
        if (person is null)
            return Result.NotFound();

        var relatedIds = person.Relationships.Select(r => r.RelatedPersonId).Distinct().ToList();
        var relatedPeople = await _personRepository.ListAsync(
            new PersonsByIdsSpecification(relatedIds),
            cancellationToken
        );
        var namesById = relatedPeople.ToDictionary(p => p.Id, p => p.Name.Value);

        var dtos = person
            .Relationships.Select(r =>
                r switch
                {
                    FamilyRelationship f => new RelationshipDto(
                        f.Type,
                        f.RelatedPersonId,
                        namesById.GetValueOrDefault(f.RelatedPersonId, "Unknown"),
                        f.GenerationOffset,
                        f.Degree,
                        f.IsByMarriage,
                        f.IsHalf
                    ),
                    FriendRelationship fr => new RelationshipDto(
                        fr.Type,
                        fr.RelatedPersonId,
                        namesById.GetValueOrDefault(fr.RelatedPersonId, "Unknown"),
                        null,
                        null,
                        null,
                        null
                    ),
                    _ => throw new InvalidOperationException(
                        $"Unhandled relationship type {r.GetType()}"
                    ),
                }
            )
            .ToList();

        return Result.Success<IEnumerable<RelationshipDto>>(dtos);
    }
}
