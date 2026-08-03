using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.Enumerations;
using KinGraph.UseCases.Persons.Relationships.Create;
using KinGraph.Web.Extensions;

namespace KinGraph.Web.Features.PersonFeatures.Relationships;

public sealed class CreateRelationshipRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int RelatedUserId { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;

    public string? Kind { get; set; }

    public int? CousinDegree { get; set; }

    public bool IsByMarriage { get; set; }

    public bool IsHalf { get; set; }
}

public class CreateEndpoint(IMediator _mediator)
    : Endpoint<
        CreateRelationshipRequest,
        Results<NoContent, ValidationProblem, NotFound, ProblemHttpResult>
    >
{
    public override void Configure()
    {
        Post("/users/{id}/relationships");

        Summary(s =>
        {
            s.Summary = "Create a relationship between two users";
            s.Description =
                "Creates a relationship from the specified user's person to another user's person. Also creates the mirrored relationship on the other side automatically (e.g. adding a Parent also adds the reciprocal Child).";
            s.ExampleRequest = new CreateRelationshipRequest
            {
                Id = 1,
                RelatedUserId = 2,
                Type = "Family",
                Kind = "Parent",
            };

            s.Responses[204] = "Relationship created successfully";
            s.Responses[400] = "Invalid request data";
            s.Responses[404] = "User not found";
        });

        Tags("Persons");

        Description(builder =>
            builder
                .Accepts<CreateRelationshipRequest>()
                .Produces(204)
                .ProducesProblem(400)
                .ProducesProblem(404)
        );
    }

    public override async Task<
        Results<NoContent, ValidationProblem, NotFound, ProblemHttpResult>
    > ExecuteAsync(CreateRelationshipRequest request, CancellationToken cancellationToken)
    {
        var callerId = HttpContext.User.GetUserId();
        if (callerId is null || callerId.Value.Value != request.Id)
        {
            return TypedResults.NotFound();
        }

        var type = Enum.Parse<RelationshipType>(request.Type, ignoreCase: true);
        FamilyRelationshipKind? kind = request.Kind is null
            ? null
            : Enum.Parse<FamilyRelationshipKind>(request.Kind, ignoreCase: true);

        var command = new CreateRelationshipCommand(
            UserId.From(request.Id),
            UserId.From(request.RelatedUserId),
            type,
            kind,
            request.CousinDegree,
            request.IsByMarriage,
            request.IsHalf
        );

        var result = await _mediator.Send(command, cancellationToken);

        return result.ToNoContentResult();
    }
}

public sealed class CreateRelationshipValidator : Validator<CreateRelationshipRequest>
{
    private static readonly string[] ValidTypes = ["Family", "Friend"];
    private static readonly string[] ValidKinds = ["Parent", "Sibling", "Cousin"];

    public CreateRelationshipValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than zero");
        RuleFor(x => x.RelatedUserId)
            .GreaterThan(0)
            .WithMessage("RelatedUserId must be greater than zero");

        RuleFor(x => x.Type)
            .Must(t => ValidTypes.Contains(t, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Type must be one of: {string.Join(", ", ValidTypes)}");

        When(
            x => string.Equals(x.Type, "Family", StringComparison.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(x => x.Kind)
                    .NotEmpty()
                    .WithMessage("Kind is required for a Family relationship.")
                    .Must(k => k is null || ValidKinds.Contains(k, StringComparer.OrdinalIgnoreCase))
                    .WithMessage($"Kind must be one of: {string.Join(", ", ValidKinds)}");

                When(
                    x => string.Equals(x.Kind, "Cousin", StringComparison.OrdinalIgnoreCase),
                    () =>
                    {
                        RuleFor(x => x.CousinDegree)
                            .NotNull()
                            .GreaterThanOrEqualTo(2)
                            .WithMessage("CousinDegree must be >= 2 for a Cousin relationship.");
                    }
                );
            }
        );
    }
}
