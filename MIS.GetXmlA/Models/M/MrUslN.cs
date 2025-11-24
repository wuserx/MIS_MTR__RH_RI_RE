// Сведения о медработниках в USL
[Table("MrUslN", Schema = "Reestr")]
public class MrUslN
{
    public int Id { get; set; }
    public int? MR_N { get; set; }
    public int? PRVS { get; set; }
    public string? CODE_MD { get; set; }

    [Required]
    public int UslId { get; set; }
    [ForeignKey("UslId")]
    public virtual Usl? Usl { get; set; }
}
