using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.UseCases.Users.Create;
using KinGraph.Web.Extensions;

namespace KinGraph.Web.Features.UserFeatures;

public sealed class CreateUserRequest
{
    [Required]
    public string Name { get; set; } = String.Empty;
    public string? PhoneNumber { get; set; } = null;
}

public sealed record CreateUserResponse(int Id, string Name);

public class CreateEndpoint(IMediator _mediator)
    : Endpoint<
        CreateUserRequest,
        Results<Created<CreateUserResponse>, ValidationProblem, ProblemHttpResult>
    >
{
    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Create a new user";
            s.Description = "Creates a new user with the specified name and phone number.";
            s.ExampleRequest = new CreateUserRequest { Name = "Sample User" };
            s.ResponseExamples[201] = new CreateUserResponse(Id: 1, Name: "Teste");

            s.Responses[201] = "User created successfully";
            s.Responses[400] = "Invalid request data";
        });

        Tags("Users");

        Description(builder =>
            builder
                .Accepts<CreateUserRequest>()
                .Produces<CreateUserResponse>(201, "application/json")
                .ProducesProblem(400)
        );
    }

    public override async Task<
        Results<Created<CreateUserResponse>, ValidationProblem, ProblemHttpResult>
    > ExecuteAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(
            UserName.From(request.Name),
            request.PhoneNumber ?? String.Empty
        );
        var result = await _mediator.Send(command, cancellationToken);

        return result.ToCreatedResult(
            id => $"/users/{id}",
            id => new CreateUserResponse(id.Value, command.Name.Value)
        );
    }
}

public sealed class CreateUserValidator : Validator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(2)
            .MaximumLength(UserName.MaxLength)
            .WithMessage($"User name must not exceed {UserName.MaxLength} characters");
    }
}
