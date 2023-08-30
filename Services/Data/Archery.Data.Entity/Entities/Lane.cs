namespace Archery.Data.Entity;

public class Lane
{
    public int ID { get; set; }
    public int Identifier { get; set; }
    public IList<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
