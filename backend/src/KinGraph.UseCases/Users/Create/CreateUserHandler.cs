
using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.ValueObjects;

namespace KinGraph.UseCases.Users.Create;

public record CreateUserCommand(UserName Name, string PhoneNumber) : ICommand<Result<UserId>>;

public class CreateUserHandler(IRepository<User> _userRepository, IRepository<Person> _personRepository)
    : ICommandHandler<CreateUserCommand, Result<UserId>>
{
    public async ValueTask<Result<UserId>> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var newPerson = Person.Create(PersonName.From(command.Name.Value));
        var createdPerson = await _personRepository.AddAsync(newPerson, cancellationToken);

        var newUser = User.Create(command.Name, createdPerson.Id);
        if (!string.IsNullOrEmpty(command.PhoneNumber))
        {
            var phoneNumber = new PhoneNumber("+1", command.PhoneNumber, String.Empty);
            newUser.UpdatePhoneNumber(phoneNumber);
        }
        var createdItem = await _userRepository.AddAsync(newUser, cancellationToken);

        return createdItem.Id;
    }
}
