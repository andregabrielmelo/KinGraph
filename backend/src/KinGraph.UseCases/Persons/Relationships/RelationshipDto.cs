using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Enumerations;

namespace KinGraph.UseCases.Persons.Relationships;

public record RelationshipDto(
    RelationshipType Type,
    PersonId RelatedPersonId,
    string RelatedPersonName,
    int? GenerationOffset,
    int? Degree,
    bool? IsByMarriage,
    bool? IsHalf
);
