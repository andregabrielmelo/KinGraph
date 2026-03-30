using KinGraph.Core.Aggregates.PersonAggregate;

namespace KinGraph.Infrastructure.Data.Configurations;

public class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
{
    public void Configure(EntityTypeBuilder<Relationship> builder)
    {
        builder.ToTable("person_relationships");

        builder.HasKey(x => x.Id);

        builder
            .Property<PersonId>("source_person_id")
            .HasConversion(id => id.Value, v => PersonId.From(v))
            .IsRequired();

        builder
            .HasOne<Person>()
            .WithMany()
            .HasForeignKey("source_person_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasDiscriminator<string>("type")
            .HasValue<FriendRelationship>("relationship_friend")
            .HasValue<FamilyRelationship>("relationship_family")
            .IsComplete(true);

        // Base properties
        builder
            .Property<PersonId>(r => r.RelatedPersonId)
            .HasConversion(id => id.Value, v => PersonId.From(v))
            .IsRequired();

        // Index for uniqueness
        builder
            .HasIndex("source_person_id", nameof(Relationship.RelatedPersonId), "type")
            .IsUnique();
    }
}

public class FamilyRelationshipConfiguration : IEntityTypeConfiguration<FamilyRelationship>
{
    public void Configure(EntityTypeBuilder<FamilyRelationship> builder)
    {
        builder.Property(f => f.GenerationOffset).HasColumnName("generation_offset").IsRequired();

        builder.Property(f => f.Degree).HasColumnName("degree").IsRequired();

        builder.Property(f => f.IsByMarriage).HasColumnName("is_by_marriage").IsRequired();

        builder.Property(f => f.IsHalf).HasColumnName("is_half").IsRequired();

        builder.Property(f => f.Gender).HasColumnName("gender").HasConversion<string>();
    }
}

public class FriendRelationshipConfiguration : IEntityTypeConfiguration<FriendRelationship>
{
    public void Configure(EntityTypeBuilder<FriendRelationship> builder)
    {
        builder.HasBaseType<Relationship>();
    }
}
