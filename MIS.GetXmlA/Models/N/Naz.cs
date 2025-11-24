[Table("Naz", Schema = "Reestr")]
public class Naz
{
    public Naz(int nazn, int nazr)
    {
        NAZ_N = nazn;
        NAZ_R = nazr;
    }

    public Naz()
    {
           
    }

    public int Id { get; set; }
    public int? NAZ_N { get; set; }
    public int? NAZ_R { get; set; }
    public int? NAZ_SP { get; set; }
    public int? NAZ_V { get; set; }
    public string? NAZ_USL { get; set; }
    public DateTime? NAPR_DATE { get; set; }
    public string? NAPR_MO { get; set; }
    public int? NAZ_PMP { get; set; }
    public int? NAZ_PK { get; set; }

    [NotMapped]
    public int? NAZ_IDDOKT { get; set; }

    public int? SluchId { get; set; }
    [ForeignKey("SluchId")]
    public virtual Sluch? Sluch { get; set; }

}