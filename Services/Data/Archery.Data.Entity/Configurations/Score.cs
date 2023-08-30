namespace Archery.Data.Entity.Configuration;

public class Score : IEntityTypeConfiguration<E.Score>
{
    private const string TableName = "Score";
    public void Configure(EntityTypeBuilder<E.Score> builder)
    {
        builder.ToTable(TableName);

        builder.HasOne<E.Player>(score => score.Player);
        builder.HasOne<E.Set>(score => score.Set).WithMany(s => s.Scores).HasForeignKey(c => c.SetID).OnDelete(DeleteBehavior.NoAction);
    }
}