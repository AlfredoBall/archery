namespace Archery.Data.Entity;

public class Score
{
    public int ID { get; set; }
    public int PlayerID { get; set; }
    public Player Player { get; set; }
    public int Points { get; set; }
    public int SetID { get; set; }
    public Set Set { get; set; } 
}
