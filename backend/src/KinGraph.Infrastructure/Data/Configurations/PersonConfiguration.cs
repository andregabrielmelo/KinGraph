using KinGraph.Core.Aggregates.PersonAggregate;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KinGraph.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons");

        builder
            .Property(x => x.Id)
            .HasValueGenerator<
                VogenIdValueGenerator<ApplicationDatabaseContext, Person, PersonId>
            >()
            .HasVogenConversion()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasVogenConversion()
            .HasMaxLength(PersonName.MaxLength)
            .IsRequired();

        builder.Property(x => x.Gender).HasConversion<string>();

        builder.Property(x => x.MaritalStatus).HasConversion<string>();

        builder
            .Property(x => x.BloodType)
            .HasConversion(bloodType => bloodType.Value, value => BloodType.FromValue(value));

        builder
            .Property(x => x.Nationality)
            .HasConversion(country => country.Alpha2Code, code => Country.FromAlpha2(code));

        builder.ComplexProperty(x => x.PhoneNumber);

        builder.ComplexProperty(x => x.EmailAddress);

        builder.ComplexProperty(
            x => x.Height,
            height =>
            {
                height.Property(h => h.Value).HasColumnName("height_value");

                height
                    .Property(h => h.Unit)
                    .HasColumnName("height_unit")
                    .HasConversion(unit => unit.Value, value => LengthUnit.FromValue(value));
            }
        );

        builder.ComplexProperty(
            x => x.Weight,
            weight =>
            {
                weight.Property(h => h.Value).HasColumnName("weight_value");

                weight
                    .Property(h => h.Unit)
                    .HasColumnName("weight_unit")
                    .HasConversion(unit => unit.Value, value => WeightUnit.FromValue(value));
            }
        );

        builder.ComplexProperty(x => x.DateOfBirth);

        builder.ComplexProperty(x => x.Occupation);

        builder.ComplexProperty(
            x => x.PlaceOfBirth,
            placeOfBirth =>
            {
                placeOfBirth.Property(a => a.Street).HasColumnName("street");
                placeOfBirth.Property(a => a.PostalCode).HasColumnName("postal_code");

                placeOfBirth.ComplexProperty(a => a.City);
                placeOfBirth.ComplexProperty(a => a.State);
                placeOfBirth
                    .Property(a => a.Country)
                    .HasConversion(c => c.Value, v => Country.FromValue(v))
                    .HasColumnName("country")
                    .IsRequired();
            }
        );

        builder
            .HasMany(x => x.Relationships)
            .WithOne()
            .HasForeignKey("source_person_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsMany(
            x => x.Addresses,
            b =>
            {
                b.ToTable("person_addresses");
                b.WithOwner().HasForeignKey("person_id");
                b.Property<int>("id");
                b.HasKey("id");

                b.Property(a => a.Street).HasColumnName("street");
                b.Property(a => a.PostalCode).HasColumnName("postal_code");

                b.OwnsOne(a => a.City);
                b.OwnsOne(a => a.State);
                b.Property(a => a.Country)
                    .HasConversion(c => c.Value, v => Country.FromValue(v))
                    .HasColumnName("country")
                    .IsRequired();
            }
        );

        builder.OwnsMany(
            x => x.Hobbies,
            b =>
            {
                b.ToTable("person_hobbies");
                b.WithOwner().HasForeignKey("person_id");
                b.Property<int>("id");
                b.HasKey("id");
            }
        );

        builder.OwnsMany(
            x => x.Citizenships,
            b =>
            {
                b.ToTable("person_citizenships");
                b.WithOwner().HasForeignKey("person_id");
                b.Property<int>("id");
                b.HasKey("id");

                b.Property(c => c.Country)
                    .HasConversion(country => country.Value, code => Country.FromValue(code));
            }
        );

        builder.OwnsMany(
            x => x.SocialMediaProfiles,
            b =>
            {
                b.ToTable("person_social_profiles");
                b.WithOwner().HasForeignKey("person_id");
                b.Property<int>("id");
                b.HasKey("id");

                b.Property(p => p.Platform)
                    .HasConversion(
                        socialMediaPlatform => socialMediaPlatform.Value,
                        value => SocialMediaPlatform.FromValue(value)
                    )
                    .IsRequired();
            }
        );

        builder.OwnsMany(
            x => x.GovernmentalDocuments,
            b =>
            {
                b.ToTable("person_documents");
                b.WithOwner().HasForeignKey("person_id");
                b.Property<int>("id");
                b.HasKey("id");

                b.Property(d => d.Type).HasConversion<string>().IsRequired();

                b.Property(d => d.IssuingCountry)
                    .HasConversion(country => country.Value, code => Country.FromValue(code));
            }
        );

        builder.OwnsMany(
            x => x.ExtraFields,
            b =>
            {
                b.ToTable("person_extra_fields");
                b.WithOwner().HasForeignKey("person_id");
                b.Property<int>("id");
                b.HasKey("id");
            }
        );

        // TODO: Is this really necessary? Is there no better way?
        builder
            .Property<List<Language>>("_languages")
            .HasConversion(
                v => v.Select(l => l.Code).ToList(),
                v => v.Select(Language.FromCode).ToList()
            )
            .Metadata.SetValueComparer(
                new ValueComparer<List<Language>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                )
            );
        builder.Ignore(p => p.Languages);

        builder.Navigation(x => x.Hobbies).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Citizenships).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Navigation(x => x.SocialMediaProfiles)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Navigation(x => x.GovernmentalDocuments)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.ExtraFields).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Addresses).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
