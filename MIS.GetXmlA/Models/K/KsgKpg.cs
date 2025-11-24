public class KsgKpg
{
    public int Id { get; set; }

    [StringLength(20)]
    public string? N_KSG { get; set; }

    [Required]
    public int VER_KSG { get; set; }

    [Required]
    public int KSG_PG { get; set; }

    public int? N_KPG { get; set; }

    public string? N_KPGN { get; set; }

    [Required]
    public decimal KOEF_Z { get; set; }

    [Required]
    public decimal KOEF_UP { get; set; }

    [Required]
    public decimal BZTSZ { get; set; }

    [Required]
    public decimal KOEF_D { get; set; }

    [Required]
    public decimal KOEF_U { get; set; }
    public decimal? KOEF_ZP { get; set; }
    public decimal? KOEF_PR { get; set; }
    public decimal? KOEF_DL { get; set; }

    [StringLength(10)]
    public string? DKK1 { get; set; }

    [StringLength(10)]
    public string? DKK2 { get; set; }

    [Required]
    public int SL_K { get; set; }
    public decimal? IT_SL { get; set; }
    public bool Surgical { get; set; }
    public bool ChemoTherapy { get; set; }
    public bool RadiationTherapy { get; set; }
    public bool ChemoRadiationTherapy { get; set; }

    
    //public virtual Sluch? Sluch { get; set; }

    public string? SluchKoefs { get; set; }

    [NotMapped]
    public List<SluchKoef>? _SluchKoefs
    {
        get { return SluchKoefs == null ? null : JsonConvert.DeserializeObject<List<SluchKoef>>(SluchKoefs); }
        set { SluchKoefs = JsonConvert.SerializeObject(value); }
    }

    public string? _crit { get; internal set; }
    [NotMapped]
    public string[]? Crits
    {
        get { return _crit == null ? null : JsonConvert.DeserializeObject<string[]>(_crit); }
        set { _crit = value == null ? null : JsonConvert.SerializeObject(value); }
    }  
}
