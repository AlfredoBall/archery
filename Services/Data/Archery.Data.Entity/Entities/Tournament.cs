namespace Archery.Data.Entity;

public class Tournament
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int LeagueID { get; set; }
    public League League { get; set; }
    public int LaneID { get; set; }
    public Lane Lane { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public IList<Reservation> Reservations { get; set; } = new List<Reservation>();
    public IList<Set> Sets { get; set; } = new List<Set>();
}