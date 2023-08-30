namespace Archery.Data.Entity.Configuration;

public class Tournament : IEntityTypeConfiguration<E.Tournament>
{
    private const string TableName = "Tournament";
    public void Configure(EntityTypeBuilder<E.Tournament> builder)
    {
        builder.ToTable(TableName);

        builder.HasIndex(t => t.Name).IsUnique();
        builder.HasMany<E.Reservation>(t => t.Reservations);
        builder.HasOne<E.Lane>(t => t.Lane);
    }
}