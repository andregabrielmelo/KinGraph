using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.UseCases.Persons.Relationships.List;
using KinGraph.Web.Extensions;

namespace KinGraph.Web.Features.PersonFeatures.Relationships;

public sealed class ListUserRelationshipsRequest
{
    [Required]
    public int Id { get; set; }
}

public sealed record RelationshipRecord(
    string Type,
    int RelatedPersonId,
    string RelatedPersonName,
    int? GenerationOffset,
    int? Degree,
    bool? IsByMarriage,
    bool? IsHalf
);

public class ListEndpoint(IMediator _mediator)
    : Endpoint<
        ListUserRelationshipsRequest,
        Results<Ok<IEnumerable<RelationshipRecord>>, NotFound, ProblemHttpResult>
    >
{
    public override void Configure()
    {
        Get("/users/{id}/relationships");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "List a user's relationships";
            s.Description =
                "Lists the relationships of the person linked to the specified user, including relationships created from the other side (e.g. being added as someone else's Parent surfaces as a Child relationship here).";
            s.ExampleRequest = new ListUserRelationshipsRequest { Id = 1 };

            s.Responses[200] = "Relationships obtained successfully";
            s.Responses[400] = "Invalid request data";
            s.Responses[404] = "User not found";
        });

        Tags("Persons");

        Description(builder =>
            builder
                .Accepts<ListUserRelationshipsRequest>()
                .Produces<IEnumerable<RelationshipRecord>>(200, "application/json")
                .ProducesProblem(400)
                .ProducesProblem(404)
        );
    }

    public override async Task<
        Results<Ok<IEnumerable<RelationshipRecord>>, NotFound, ProblemHttpResult>
    > ExecuteAsync(ListUserRelationshipsRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new ListUserRelationshipsQuery(UserId.From(request.Id)),
            cancellationToken
        );

        return result.ToGetByIdResult(dtos =>
            dtos.Select(d => new RelationshipRecord(
                d.Type.ToString(),
                d.RelatedPersonId.Value,
                d.RelatedPersonName,
                d.GenerationOffset,
                d.Degree,
                d.IsByMarriage,
                d.IsHalf
            ))
        );
    }
}

public sealed class ListUserRelationshipsValidator : Validator<ListUserRelationshipsRequest>
{
    public ListUserRelationshipsValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than zero");
    }
}
