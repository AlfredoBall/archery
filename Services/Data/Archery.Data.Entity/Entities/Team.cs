namespace Archery.Data.Entity;

public class Team
{
    public int ID { get; set; }
    public string Name { get; set; }
    public IList<Reservation> Reservations { get; set; } = new List<Reservation>();
    public IList<Player> Players { get; set; } = new List<Player>();
}