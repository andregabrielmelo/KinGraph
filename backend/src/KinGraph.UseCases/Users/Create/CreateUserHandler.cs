using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.Aggregates.UserAggregate.Specifications;
using KinGraph.Core.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace KinGraph.UseCases.Users.Create;

public record CreateUserCommand(
    UserName Name,
    EmailAddress Email,
    string Password,
    string PhoneNumber
) : ICommand<Result<UserId>>;

public class CreateUserHandler(
    IRepository<User> _userRepository,
    IRepository<Person> _personRepository,
    IPasswordHasher<User> _passwordHasher
) : ICommandHandler<CreateUserCommand, Result<UserId>>
{
    public async ValueTask<Result<UserId>> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        // TODO: Review if this is really the place to check for existing user, or if it should be done in the domain layer
        var existingUser = await _userRepository.FirstOrDefaultAsync(
            new UserByEmailSpecification(command.Email),
            cancellationToken
        );
        if (existingUser is not null)
        {
            return Result<UserId>.Invalid(
                new ValidationError(nameof(command.Email), "Email is already registered")
            );
        }

        var newPerson = Person.Create(PersonName.From(command.Name.Value));
        var createdPerson = await _personRepository.AddAsync(newPerson, cancellationToken);

        var password = _passwordHasher.HashPassword(null!, command.Password);
        var newUser = User.Create(command.Name, createdPerson.Id, command.Email, password);
        if (!string.IsNullOrEmpty(command.PhoneNumber))
        {
            var phoneNumber = new PhoneNumber("+1", command.PhoneNumber, String.Empty);
            newUser.UpdatePhoneNumber(phoneNumber);
        }
        var createdItem = await _userRepository.AddAsync(newUser, cancellationToken);

        return createdItem.Id;
    }
}
