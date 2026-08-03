using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.UseCases.Persons;
using KinGraph.UseCases.Persons.GetOwnProfile;
using KinGraph.Web.Extensions;

namespace KinGraph.Web.Features.PersonFeatures.Profile;

public sealed class GetProfileRequest
{
    [Required]
    public int Id { get; set; }
}

public sealed record ProfileRecord(
    string Name,
    string? Gender,
    DateTime? DateOfBirth,
    string? OccupationName,
    decimal? OccupationSalary
);

public class GetEndpoint(IMediator _mediator)
    : Endpoint<GetProfileRequest, Results<Ok<ProfileRecord>, NotFound, ProblemHttpResult>>
{
    public override void Configure()
    {
        Get("/users/{id}/person");

        Summary(s =>
        {
            s.Summary = "Get your own profile";
            s.Description = "Gets the profile of the person linked to the specified user. Only the authenticated user themselves may view it.";
            s.ExampleRequest = new GetProfileRequest { Id = 1 };

            s.Responses[200] = "Profile obtained successfully";
            s.Responses[400] = "Invalid request data";
            s.Responses[404] = "User not found, or the route id does not match the authenticated user";
        });

        Tags("Persons");

        Description(builder =>
            builder
                .Accepts<GetProfileRequest>()
                .Produces<ProfileRecord>(200, "application/json")
                .ProducesProblem(400)
                .ProducesProblem(404)
        );
    }

    public override async Task<Results<Ok<ProfileRecord>, NotFound, ProblemHttpResult>> ExecuteAsync(
        GetProfileRequest request,
        CancellationToken cancellationToken
    )
    {
        var callerId = HttpContext.User.GetUserId();
        if (callerId is null || callerId.Value.Value != request.Id)
        {
            return TypedResults.NotFound();
        }

        var result = await _mediator.Send(
            new GetOwnProfileQuery(UserId.From(request.Id)),
            cancellationToken
        );

        return result.ToGetByIdResult(dto => new ProfileRecord(
            dto.Name,
            dto.Gender?.ToString(),
            dto.DateOfBirth,
            dto.OccupationName,
            dto.OccupationSalary
        ));
    }
}

public sealed class GetProfileValidator : Validator<GetProfileRequest>
{
    public GetProfileValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than zero");
    }
}
