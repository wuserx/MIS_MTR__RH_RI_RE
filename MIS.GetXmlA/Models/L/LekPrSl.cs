// сведения о лекарственном препарате
[Table("LekPrSl", Schema = "Reestr")]
public class LekPrSl
{
    [Key]
    public int Id { get; set; }
    public DateTime? DATA_INJ { get; set; }
    public string? CODE_SH { get; set; } // из модели V032 CombTreat поле ScheDrugGrCd ---  в модели М030 TreatReg  проверять только до третьей  "-" 1-2-5  1-2  
    public string? REGNUM { get; set; } // из модели V033 DgTreatReg поле DrugCode - с учетом схемы
    public string? COD_MARK { get; set; }


    //[Required]
    //public int SluchId { get; set; }
    //[ForeignKey("SluchId")]
    //public virtual Sluch? Sluch { get; set; }


    private ICollection<LekDose>? lekDoses;
    public virtual ICollection<LekDose>? LekDoses
    {
        get { return lekDoses ?? (lekDoses = new Collection<LekDose>()); }
        set { lekDoses = value; }
    }

}