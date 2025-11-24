[Table("BDiag", Schema = "Reestr")]
public class BDiag
{
    public int Id { get; set; }
    public DateTime? DIAG_DATE { get; set; }
    public int? DIAG_TIP { get; set; }
    public int? DIAG_CODE { get; set; }
    public int? DIAG_RSLT { get; set; }
    public int? REC_RSLT { get; set; }

    [Required]
    public int OnkSlId { get; set; }

    //[ForeignKey("OnkSlId")]
    //public virtual OnkSl? OnkSl { get; set; }
}