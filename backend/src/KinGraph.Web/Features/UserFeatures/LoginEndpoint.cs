using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KinGraph.Core.ValueObjects;
using KinGraph.UseCases.Users.Login;
using KinGraph.Web.Configurations;
using KinGraph.Web.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KinGraph.Web.Features.UserFeatures;

public sealed class LoginRequest
{
    [Required]
    public string Email { get; set; } = String.Empty;

    [Required]
    public string Password { get; set; } = String.Empty;
}

public sealed record LoginResponse(string Token, int UserId, string Name);

public class LoginEndpoint(IMediator _mediator, IOptions<JwtOptions> _jwtOptions)
    : Endpoint<
        LoginRequest,
        Results<Ok<LoginResponse>, UnauthorizedHttpResult, ProblemHttpResult>
    >
{
    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Log in";
            s.Description = "Authenticates a user by email/password and returns a JWT.";
            s.ExampleRequest = new LoginRequest { Email = "sample.user@example.com", Password = "Passw0rd!" };

            s.Responses[200] = "Login successful";
            s.Responses[400] = "Invalid request data";
            s.Responses[401] = "Invalid email or password";
        });

        Tags("Users");

        Description(builder =>
            builder
                .Accepts<LoginRequest>()
                .Produces<LoginResponse>(200, "application/json")
                .ProducesProblem(400)
                .ProducesProblem(401)
        );
    }

    public override async Task<
        Results<Ok<LoginResponse>, UnauthorizedHttpResult, ProblemHttpResult>
    > ExecuteAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new LoginQuery(new EmailAddress(request.Email), request.Password),
            cancellationToken
        );

        return result.ToLoginResult(user => new LoginResponse(
            BuildToken(user.Id.Value, user.Name.Value),
            user.Id.Value,
            user.Name.Value
        ));
    }

    private string BuildToken(int userId, string name)
    {
        var options = _jwtOptions.Value;
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, name),
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(options.ExpirationMinutes),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public sealed class LoginValidator : Validator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Email must be a valid email address");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}
