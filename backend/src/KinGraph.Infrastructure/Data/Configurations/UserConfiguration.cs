using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.ValueObjects;

namespace KinGraph.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(entity => entity.Id)
            .HasValueGenerator<VogenIdValueGenerator<ApplicationDatabaseContext, User, UserId>>()
            .HasVogenConversion()
            .IsRequired();

        builder
            .Property(entity => entity.Name)
            .HasVogenConversion()
            .HasMaxLength(UserName.MaxLength)
            .IsRequired();

        builder.Property(entity => entity.PersonId).HasVogenConversion().IsRequired();

        builder.HasIndex(entity => entity.PersonId).IsUnique();

        builder
            .Property(entity => entity.Email)
            .HasConversion(email => email.Value, value => new EmailAddress(value))
            .HasMaxLength(320)
            .IsRequired();

        builder.HasIndex(entity => entity.Email).IsUnique();

        builder.Property(entity => entity.PasswordHash).IsRequired();

        builder
            .HasOne<Person>()
            .WithOne()
            .HasForeignKey<User>(user => user.PersonId)
            .HasPrincipalKey<Person>(person => person.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(builder => builder.PhoneNumber);
    }
}
