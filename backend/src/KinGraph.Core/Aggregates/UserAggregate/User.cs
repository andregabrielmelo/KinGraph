namespace KinGraph.Core.Aggregates.UserAggregate;

public class User(UserName name) : EntityBase<User, UserId>, IAggregateRoot
{
    public UserName Name { get; private set; } = name;
    public PhoneNumber? PhoneNumber { get; private set; }

    public static User Create(UserName name) => new User(name);

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
