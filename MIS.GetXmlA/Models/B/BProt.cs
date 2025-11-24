[Table("BProt", Schema = "Reestr")]
public class BProt
{
    public int Id { get; set; }
    public int? PROT { get; set; }
    public DateTime? D_PROT { get; set; }

    [Required]
    public int OnkSlId { get; set; }

    //[ForeignKey("OnkSlId")]
    //public virtual OnkSl? OnkSl { get; set; }
}
