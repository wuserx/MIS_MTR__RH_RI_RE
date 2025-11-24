//Сведения о мед изделиях
[Table("MedDev", Schema = "Reestr")]
public class MedDev
{
    public int Id { get; set; }
    public DateTime? DATE_MED { get; set; }
    public int? CODE_MEDDEV { get; set; } // 1079 МЗ
    public string? NUMBER_SER { get; set; }

    [Required]
    public int UslId { get; set; }
    //[ForeignKey("UslId")]
    //public virtual Usl? Usl { get; set; }
}