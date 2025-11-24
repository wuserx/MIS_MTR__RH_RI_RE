[Table("OnkUsl", Schema = "Reestr")]
public class OnkUsl
{
    public OnkUsl() { }

    public OnkUsl(int usltip)
    {
        USL_TIP = usltip;
    }

    public int Id { get; set; }

    [Required]
    public int OnkSlId { get; set; }

    //[ForeignKey("OnkSlId")]
    //public virtual OnkSl OnkSl { get; set; }

    [Required]
    public int USL_TIP { get; set; }
    public int? HIR_TIP { get; set; }
    public int? LEK_TIP_L { get; set; }
    public int? LEK_TIP_V { get; set; }
    public int? PPTR { get; set; }
    public int? LUCH_TIP { get; set; }

    // B_DIAG
    public string? _LekPrs { get; internal set; }
    [NotMapped]
    public List<LekPr2>? LekPrs
    {
        get { return _LekPrs == null ? null : JsonConvert.DeserializeObject<List<LekPr2>>(_LekPrs); }
        set { _LekPrs = JsonConvert.SerializeObject(value); }
    }

    //LEkPr
    private ICollection<LEkPr>? lekprs;
    public virtual ICollection<LEkPr>? LEkPrs
    {
        get { return lekprs ?? (lekprs = new Collection<LEkPr>()); }
        set { lekprs = value; }
    }
}
