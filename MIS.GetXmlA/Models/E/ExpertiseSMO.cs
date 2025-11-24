
[Table("ExpertiseSMO", Schema = "Reestr")]
[Index("ZakSluchId", IsUnique = false, Name = "ZakSluchId_Index")]
public class ExpertiseSMO
{
    public int Id { get; set; }

    public int ExpertiseSMOPacketId { get; set; }
    public virtual ExpertiseSMOPacket ExpertiseSMOPacket { get; set; }

    public int? SchetId { get; set; }
    public int? ZakSluchId { get; set; }
    public int? CODE { get; set; }
    public string CODE_MO { get; set; }
    public int YEAR { get; set; }
    public int MONTH { get; set; }
    public string NSCHET { get; set; }
    public DateTime DSCHET { get; set; }

    public string? EXP_CODE { get; set; }
    public long IDCASE { get; set; }
    public string? SL_ID { get; set; }
    public string? NHISTORY { get; set; }
    public string? NPOLIS { get; set; }
    public decimal SUMV { get; set; }
    public int OPLATA { get; set; }
    public decimal SUMP { get; set; }
    public decimal SANK_MEE { get; set; }
    public decimal SANK_EKMP { get; set; }
    public decimal SHTRAF { get; set; }
    public string? REFREASON { get; set; }
    public int? VID_EXPERT { get; set; }
    public int? TYPE_EXPERT { get; set; }
    public DateTime DATE_E { get; set; }
    public string? SNILS { get; set; }
    public string? FIO { get; set; }
    public string? ZAKL { get; set; }
    public string FILENAMEEXP { get; set; }

}
