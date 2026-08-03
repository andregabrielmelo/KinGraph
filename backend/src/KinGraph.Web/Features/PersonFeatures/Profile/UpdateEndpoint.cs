using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.Enumerations;
using KinGraph.UseCases.Persons.UpdateOwnProfile;
using KinGraph.Web.Extensions;

namespace KinGraph.Web.Features.PersonFeatures.Profile;

public sealed class UpdateProfileRequest
{
    [Required]
    public int Id { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? OccupationName { get; set; }

    public decimal? OccupationSalary { get; set; }
}

public class UpdateEndpoint(IMediator _mediator)
    : Endpoint<UpdateProfileRequest, Results<Ok<ProfileRecord>, NotFound, ProblemHttpResult>>
{
    public override void Configure()
    {
        Put("/users/{id}/person");

        Summary(s =>
        {
            s.Summary = "Update your own profile";
            s.Description = "Updates the profile of the person linked to the specified user. Only the authenticated user themselves may update it. Fields left null are left unchanged.";
            s.ExampleRequest = new UpdateProfileRequest
            {
                Id = 1,
                Gender = "Female",
                DateOfBirth = new DateTime(1990, 4, 12),
                OccupationName = "Architect",
            };

            s.Responses[200] = "Profile updated successfully";
            s.Responses[400] = "Invalid request data";
            s.Responses[404] = "User not found, or the route id does not match the authenticated user";
        });

        Tags("Persons");

        Description(builder =>
            builder
                .Accepts<UpdateProfileRequest>()
                .Produces<ProfileRecord>(200, "application/json")
                .ProducesProblem(400)
                .ProducesProblem(404)
        );
    }

    public override async Task<
        Results<Ok<ProfileRecord>, NotFound, ProblemHttpResult>
    > ExecuteAsync(UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        var callerId = HttpContext.User.GetUserId();
        if (callerId is null || callerId.Value.Value != request.Id)
        {
            return TypedResults.NotFound();
        }

        Gender? gender = request.Gender is null
            ? null
            : Enum.Parse<Gender>(request.Gender, ignoreCase: true);

        var command = new UpdateOwnProfileCommand(
            UserId.From(request.Id),
            gender,
            request.DateOfBirth,
            request.OccupationName,
            request.OccupationSalary
        );

        var result = await _mediator.Send(command, cancellationToken);

        return result.ToUpdateResult(dto => new ProfileRecord(
            dto.Name,
            dto.Gender?.ToString(),
            dto.DateOfBirth,
            dto.OccupationName,
            dto.OccupationSalary
        ));
    }
}

public sealed class UpdateProfileValidator : Validator<UpdateProfileRequest>
{
    private static readonly string[] ValidGenders = ["Male", "Female"];

    public UpdateProfileValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than zero");

        RuleFor(x => x.Gender)
            .Must(g => g is null || ValidGenders.Contains(g, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Gender must be one of: {string.Join(", ", ValidGenders)}");
    }
}
