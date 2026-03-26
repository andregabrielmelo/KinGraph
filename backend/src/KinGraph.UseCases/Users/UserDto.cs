using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.ValueObjects;

namespace KinGraph.UseCases.Users;

public record UserDto(UserId Id, UserName Name, PhoneNumber PhoneNumber);
