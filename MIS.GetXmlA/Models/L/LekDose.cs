// сведения о дозе введенного лекарственногпрепарата
[Table("LekDose", Schema = "Reestr")]
public class LekDose
{
    public int Id { get; set; }
    public string ED_IZM { get; set; } // из модели V034 - UnitMeas поле UnitCode (Справочник умер только родившись)
    public decimal DOSE_INJ { get; set; }
    public string METHOD_INJ { get; set; } //1468 - МЗ  
    public int COL_INJ { get; set; }

    [Required]
    public int LekPrSlId { get; set; }
    [ForeignKey("LekPrSlId")]
    public virtual LekPrSl LekPrSl { get; set; }

}
