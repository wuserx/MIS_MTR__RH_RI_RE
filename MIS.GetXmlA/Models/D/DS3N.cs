[NotMapped]
public class DS3N
{
    public DS3N()
    { }
        public DS3N(string ds3, int prDS3N)
    {
        Ds3 = ds3;
        PR_DS3_N = prDS3N;
    }
    public int Id { get; set; }
    public string? Ds3 { get; set; }
    public int? DS3_PR { get; set; }
    public int? PR_DS3_N { get; set; }
    public int? SluchId { get; set; }

    [ForeignKey("SluchId")]
    public virtual Sluch? Sluch { get; set; }
}
