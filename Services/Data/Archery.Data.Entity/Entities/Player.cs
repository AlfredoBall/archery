namespace Archery.Data.Entity;
public class Player
{
    public int ID { get; set; }
    public int TeamID { get; set; }
    public Team Team { get; set; }
    public int MemberID { get; set; }
    public Member Member { get; set; }
    public IList<Score> Scores { get; set; }
}