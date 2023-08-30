namespace Archery.Data.Entity.Configuration;

public class League : IEntityTypeConfiguration<E.League>
{
    private const string TableName = "League";
    public void Configure(EntityTypeBuilder<E.League> builder)
    {
        builder.ToTable(TableName);

        builder.HasIndex(l => l.Name).IsUnique();
        builder.HasMany<E.Tournament>(league => league.Tournaments);
    }
}