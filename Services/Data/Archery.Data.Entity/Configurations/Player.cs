namespace Archery.Data.Entity.Configuration;

public class Player : IEntityTypeConfiguration<E.Player>
{
    private const string TableName = "Player";
    public void Configure(EntityTypeBuilder<E.Player> builder)
    {
        builder.ToTable(TableName);

        builder.HasIndex(p => new { p.TeamID, p.MemberID }).IsUnique();
        builder.HasOne<E.Member>(p => p.Member);
        builder.HasMany<E.Score>(p => p.Scores);
    }
}