namespace Archery.Data.Entity;

public class Set
{
    public int ID { get; set; }
    public int Ordinal { get; set; }
    public int TournamentID { get; set; }
    public Tournament Tournament { get; set; }
    public IList<Score> Scores { get; set; }
}
