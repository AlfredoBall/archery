namespace Archery.Data.Entity.Configuration;

public class Set : IEntityTypeConfiguration<E.Set>
{
    private const string TableName = "Set";
    public void Configure(EntityTypeBuilder<E.Set> builder)
    {
        builder.ToTable(TableName);

        builder.HasIndex(s => new { s.Ordinal, s.TournamentID }).IsUnique();
        builder.HasOne<E.Tournament>(s => s.Tournament);
        builder.HasMany<E.Score>(s => s.Scores);
    }
}