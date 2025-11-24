[Table("NApr", Schema = "Reestr")]
public class NApr
{
    public int Id { get; set; }
    public DateTime? NAPR_DATE { get; set; }
    public string? NAPR_MO { get; set; }
    public int? NAPR_V { get; set; }
    public int? MET_ISSL { get; set; }
    public string? NAPR_USL { get; set; }

    [Required]
    public int SluchId { get; set; }
   // [ForeignKey("SluchId")]
    //public virtual Sluch? Sluch { get; set; }
}