namespace Archery.Data.Entity.Configuration;

public class Team : IEntityTypeConfiguration<E.Team>
{
    private const string TableName = "Team";
    public void Configure(EntityTypeBuilder<E.Team> builder)
    {
        builder.ToTable(TableName);

        builder.HasIndex(t => t.Name).IsUnique();
        builder.HasMany<E.Player>(t => t.Players);
        builder.HasMany<E.Reservation>(t => t.Reservations);
    }
}