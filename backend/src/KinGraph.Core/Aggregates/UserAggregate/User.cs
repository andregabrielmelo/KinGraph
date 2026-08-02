using KinGraph.Core.Aggregates.PersonAggregate;

namespace KinGraph.Core.Aggregates.UserAggregate;

public class User(UserName name, PersonId personId) : EntityBase<User, UserId>, IAggregateRoot
{
    public UserName Name { get; private set; } = name;
    public PersonId PersonId { get; private set; } = personId;
    public PhoneNumber? PhoneNumber { get; private set; }

    public static User Create(UserName name, PersonId personId) => new User(name, personId);

    public User UpdateName(UserName newName)
    {
        if (Name == newName)
        {
            return this;
        }

        Name = newName;
        return this;
    }

    public User UpdatePhoneNumber(PhoneNumber newPhoneNumber)
    {
        PhoneNumber = newPhoneNumber;
        return this;
    }
}
