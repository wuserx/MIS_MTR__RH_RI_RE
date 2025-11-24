public class LekPr2
{
    public int? Id { get; set; }

    public string? REGNUM { get; set; }

    public string? CODE_SH { get; set; }

    public DateTime[]? date_injs { get; set; }

    public string? _date_injs { get; set; }

    [NotMapped]
    public DateTime[]? date_injs2
    {
        get { return _date_injs == null ? null : JsonConvert.DeserializeObject<DateTime[]>(_date_injs); }
        set { _date_injs = value == null ? null : JsonConvert.SerializeObject(value); }
    }
}