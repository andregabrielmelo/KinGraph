using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.UserAggregate;
using KinGraph.Core.ValueObjects;

namespace KinGraph.Web.Features.Users.Create;

public sealed class CreateUserRequest
{
    [Required]
    public string Name { get; set; } = String.Empty;
    public string? PhoneNumber { get; set; } = null;
}

public class CreateContributorResponse(int id, string name)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
}

public class CreateEndpoint(IRepository<User> repository)
    : Endpoint<
        CreateUserRequest,
        Results<Created<CreateContributorResponse>, ValidationProblem, ProblemHttpResult>
    >
{
    private readonly IRepository<User> _repository = repository;

    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Create a new user";
            s.Description = "Creates a new user with the specified name and unit price.";
            s.ExampleRequest = new CreateUserRequest { Name = "Sample User" };
            s.ResponseExamples[201] = new UserRecord(1, "Sample User", null);

            s.Responses[201] = "User created successfully";
            s.Responses[400] = "Invalid request data";
        });

        Tags("Users");

        Description(builder =>
            builder
                .Accepts<CreateUserRequest>()
                .Produces<CreateContributorResponse>(201, "application/json")
                .ProducesProblem(400)
        );
    }

    public override async Task<
        Results<Created<CreateContributorResponse>, ValidationProblem, ProblemHttpResult>
    > ExecuteAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        // TODO: Porque estou tendo que usar Core.UserAggregate.User?
        User user = Core.Aggregates.UserAggregate.User.Create(UserName.From(request.Name));
        if (!string.IsNullOrEmpty(request.PhoneNumber))
        {
            var phoneNumber = new PhoneNumber("+1", request.PhoneNumber, String.Empty);
            user.UpdatePhoneNumber(phoneNumber);
        }

        await _repository.AddAsync(user, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        var response = new CreateContributorResponse(user.Id.Value, user.Name.Value);
        return TypedResults.Created($"/Users/{user.Id.Value}", response);
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
