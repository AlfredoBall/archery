namespace Archery.Data.Entity;

public class Context : DbContext
{
    public Context() : base() { }

    public Context(DbContextOptions<Context> options) : base(options)
    {

    }

    public DbSet<League> Leagues { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Set> Sets { get; set; }

    //TODO: Singular
    public DbSet<Lane> Lanes { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Member> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new C.League());
        modelBuilder.ApplyConfiguration(new C.Lane());
        modelBuilder.ApplyConfiguration(new C.Tournament());
        modelBuilder.ApplyConfiguration(new C.Team());
        modelBuilder.ApplyConfiguration(new C.Reservation());
        modelBuilder.ApplyConfiguration(new C.Set());
        modelBuilder.ApplyConfiguration(new C.Score());
        modelBuilder.ApplyConfiguration(new C.Player());
        modelBuilder.ApplyConfiguration(new C.Member());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();

        base.OnConfiguring(optionsBuilder);
    }
}