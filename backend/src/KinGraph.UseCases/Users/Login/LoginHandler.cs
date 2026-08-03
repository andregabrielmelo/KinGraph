using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.Aggregates.UserAggregate.Specifications;
using KinGraph.Core.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace KinGraph.UseCases.Users.Login;

public record LoginQuery(EmailAddress Email, string Password) : IQuery<Result<UserDto>>;

public class LoginHandler(IRepository<User> _userRepository, IPasswordHasher<User> _passwordHasher)
    : IQueryHandler<LoginQuery, Result<UserDto>>
{
    public async ValueTask<Result<UserDto>> Handle(
        LoginQuery query,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.FirstOrDefaultAsync(
            new UserByEmailSpecification(query.Email),
            cancellationToken
        );

        // Don't distinguish "no such email" from "wrong password" - both are Unauthorized.
        if (user is null)
        {
            return Result<UserDto>.Unauthorized();
        }

        var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, query.Password);
        if (verification == PasswordVerificationResult.Failed)
        {
            return Result<UserDto>.Unauthorized();
        }

        return new UserDto(user.Id, user.Name, user.PhoneNumber ?? PhoneNumber.Unknown);
    }
}
