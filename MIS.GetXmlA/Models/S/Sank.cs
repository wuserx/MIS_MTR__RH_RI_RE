[Table("Sank", Schema = "Reestr")]
public class Sank
{
    public Sank(string scode, decimal ssum, 
                int stip, int sosn, int sist)
    {
        S_CODE = scode;
        S_SUM = ssum;
        S_TIP = stip;
        S_OSN = sosn;
        S_IST = sist;
    }

    public Sank(){}

    public int Id { get; set; }

    [Required, StringLength(36)]
    public string S_CODE { get; set; }

    [Required]
    public decimal S_SUM { get; set; }

    [Required]
    public int S_TIP { get; set; }

    [Required]
    public int S_OSN { get; set; }

    [StringLength(250)]
    public string S_COM { get; set; }

    [Required]
    public int S_IST { get; set; }

    [Required]
    public int SluchId { get; set; }
    [ForeignKey("SluchId")]
    public virtual Sluch Sluch { get; set; }

    public bool SanctionAnotherTerritory { get; set; }
}
