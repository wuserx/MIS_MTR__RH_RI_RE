[Table("DS2N", Schema = "Reestr")]
public class DS2N
{
    public DS2N()
    { }
        public DS2N(string ds2, int prds2n)
    {
        Ds2 = ds2;
        PR_DS2_N = prds2n;
    }
    public int Id { get; set; }
    public string? Ds2 { get; set; }
    public int? DS2_PR { get; set; }
    public int? PR_DS2_N { get; set; }
    public int? SluchId { get; set; }

    [ForeignKey("SluchId")]
    public virtual Sluch? Sluch { get; set; }
}
