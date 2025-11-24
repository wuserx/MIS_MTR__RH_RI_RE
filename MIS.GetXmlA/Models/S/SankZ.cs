public class SankZ
{
    public SankZ() { }

    public int Id { get; set; }

    [Required, StringLength(36)]
    public string? S_CODE { get; set; }

    [Required]
    public decimal? S_SUM { get; set; }

    [Required]
    public int? S_TIP { get; set; }

    public int? S_OSN { get; set; }

    public string? S_COM { get; set; }

    [Required]
    public  DateTime? DATE_ACT { get; set; }

    [Required]
    public string? NUM_ACT { get; set; }

    [Required]
    public int? S_IST { get; set; }

    [NotMapped]
    public int? STRAF { get; set; }

    public string? SL_ID { get; set; }

    public string? CODE_EXP { get; set; }

    public bool? IS_ACCEPTED { get; set; }

    [Required]
    public int ZakSluchId { get; set; }
    [ForeignKey("ZakSluchId")]
    public virtual ZakSluch? Zak { get; set; }

    public string? _slid { get; internal set; }
    [NotMapped]
    public string[]? SlIds
    {
        get { return _slid == null ? null : JsonConvert.DeserializeObject<string[]>(_slid); }
        set { _slid = value == null ? null : JsonConvert.SerializeObject(value); }
    }


    public bool SanctionAnotherTerritory { get; set; }

    [NotMapped]
    public bool HasOther
    {
        get { return SanctionAnotherTerritory == true ? true : false; }
    }
}
