namespace Archery.Data.Entity;

public class Reservation
{
    public int ID { get; set; }
    public int TournamentID { get; set; }
    public Tournament Tournament { get; set; }
    public int TeamID { get; set; }
    public Team Team { get; set; }
}
