using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.ValueObjects;

namespace KinGraph.UseCases.Users.Update;

public record UpdateUserCommand(UserId UserId, UserName UserName, PhoneNumber? PhoneNumber)
    : Mediator.ICommand<Result<UserDto>>;

public class UpdateUserHandler(IRepository<User> _repository)
    : Mediator.ICommandHandler<UpdateUserCommand, Result<UserDto>>
{
    public async ValueTask<Result<UserDto>> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
            return Result<UserDto>.NotFound();

        user.UpdateName(request.UserName);
        user.UpdatePhoneNumber(request.PhoneNumber);

        await _repository.UpdateAsync(user, cancellationToken);

        var dto = new UserDto(user.Id, user.Name, user.PhoneNumber);
        return Result<UserDto>.Success(dto);
    }
}
