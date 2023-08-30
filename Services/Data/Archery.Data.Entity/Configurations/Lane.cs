namespace Archery.Data.Entity.Configuration;

public class Lane : IEntityTypeConfiguration<E.Lane>
{
    private const string TableName = "Lane";
    public void Configure(EntityTypeBuilder<E.Lane> builder)
    {
        builder.ToTable(TableName);

        builder.HasIndex(l => l.Identifier).IsUnique();
        builder.HasMany<E.Tournament>(l => l.Tournaments);
    }
}