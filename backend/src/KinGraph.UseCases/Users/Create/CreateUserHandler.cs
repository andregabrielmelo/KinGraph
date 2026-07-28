using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.ValueObjects;

namespace KinGraph.UseCases.Users.Create;

public record CreateUserCommand(UserName Name, string PhoneNumber) : ICommand<Result<UserId>>;

public class CreateUserHandler(IRepository<User> _repository)
    : ICommandHandler<CreateUserCommand, Result<UserId>>
{
    public async ValueTask<Result<UserId>> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var newUser = User.Create(command.Name);
        if (!string.IsNullOrEmpty(command.PhoneNumber))
        {
            var phoneNumber = new PhoneNumber("+1", command.PhoneNumber, String.Empty);
            newUser.UpdatePhoneNumber(phoneNumber);
        }
        var createdItem = await _repository.AddAsync(newUser, cancellationToken);

        return createdItem.Id;
    }
}
