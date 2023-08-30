namespace Archery.Data.Entity.Configuration;

public class Member : IEntityTypeConfiguration<E.Member>
{
    private const string TableName = "Member";
    public void Configure(EntityTypeBuilder<E.Member> builder)
    {
        builder.ToTable(TableName);
    }
}