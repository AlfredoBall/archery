namespace Archery.Data.Entity.Configuration;

public class Reservation : IEntityTypeConfiguration<E.Reservation>
{
    private const string TableName = "Reservation";
    public void Configure(EntityTypeBuilder<E.Reservation> builder)
    {
        builder.ToTable(TableName);

        builder.HasOne<E.Tournament>(r => r.Tournament).WithMany(t => t.Reservations).HasForeignKey(r => r.TournamentID).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<E.Team>(r => r.Team).WithMany(t => t.Reservations).HasForeignKey(r => r.TeamID).OnDelete(DeleteBehavior.NoAction); ;
    }
}