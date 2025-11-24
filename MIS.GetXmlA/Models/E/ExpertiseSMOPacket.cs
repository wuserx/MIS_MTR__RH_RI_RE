[Table("ExpertiseSMOPacket", Schema = "Reestr")]
public class ExpertiseSMOPacket
{
    public int Id { get; set; }
    public string SMO { get; set; }
    public string FileName { get; set; }
    public DateTime  Date { get; set; }
    public int YEAR { get; set; }
    public int MONTH { get; set; }

    public int SD_Z { get; set; }
    public int SD_Z_MEE { get; set; }
    public int SD_Z_EKMP { get; set; }

    public int SANK_SD_Z_MEE { get; set; }
    public int SANK_SD_Z_EKMP { get; set; }

    public decimal SANK_MEE { get; set; }
    public decimal SANK_EKMP { get; set; }
    public decimal SHTRAF { get; set; }
    public string? UserName { get; set; }
    public bool YesExpertise { get; set; }

    public int? LOCK { get; set; }


    private ICollection<ExpertiseSMO> expertiseSMOs;
    public virtual ICollection<ExpertiseSMO> ExpertiseSMOs
    {
        get { return expertiseSMOs ?? (expertiseSMOs = new Collection<ExpertiseSMO>()); }
        set { expertiseSMOs = value; }
    }
}
