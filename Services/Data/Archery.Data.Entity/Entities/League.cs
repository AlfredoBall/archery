namespace Archery.Data.Entity;

public class League
{
    public int ID { get; set; }
    public string Name { get; init; }
    public IList<Tournament> Tournaments { get; set; } = new List<Tournament>();
}