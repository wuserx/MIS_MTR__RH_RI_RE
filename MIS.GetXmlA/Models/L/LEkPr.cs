using MIS.Models.I;

[Table("LEkPr", Schema = "Reestr")]
public class LEkPr
{
    public int Id { get; set; }
    public string? REGNUM { get; set; }

    public string? CODE_SH { get; set; }

    public string? REGNUM_DOP { get; set; }

    public string? _date_injs { get; internal set; }
    [NotMapped]
    public DateTime[]? date_injs
    {
        get { return _date_injs == null ? null : JsonConvert.DeserializeObject<DateTime[]>(_date_injs); }
        set { _date_injs = value == null ? null : JsonConvert.SerializeObject(value); }
    }

    //INJ
    private ICollection<INJ> iNJs;
    public virtual ICollection<INJ> INJs
    {
        get { return iNJs ?? (iNJs = new Collection<INJ>()); }
        set { iNJs = value; }
    }

    [Required]
    public int OnkUslId { get; set; }

    [ForeignKey("OnkUslId")]
    public virtual OnkUsl? OnkUsl { get; set; }
}
