// Сведения об услуге

public class Usl_mtr
{
    public int Id { get; set; }

    [Required, StringLength(36)]
    public string? IDSERV { get; set; }

    public string? CODE_UA { get; set; }

    public int? USL_LEVEL { get; set; }

    [Required, StringLength(6)]
    public string? LPU { get; set; }   

    public int? PROFIL { get; set; }

    [StringLength(15)]
    public string? VID_VME { get; set; }

    public int? DET { get; set; }

    [Required]
    public DateTime DATE_IN { get; set; }

    [Required]
    public DateTime DATE_OUT { get; set; }

    public int? P_OTK { get; set; }

    [Required, StringLength(10)]
    public string? DS { get; set; }

    [Required, StringLength(20)]
    public string? CODE_USL { get; set; }

    public string? CODE_USL_UNLOAD { get; set; }

    [Required, StringLength(254)]
    public string? USL { get; set; }

    [Required]
    public decimal? KOL_USL { get; set; }

    public decimal? TARIF { get; set; }

    [Required]
    public decimal? SUMV_USL { get; set; }

        
    public int? PRVS { get; set; }

    [StringLength(250)]
    public string? COMENTU { get; set; }

    public string? LPU_1 { get; set; }

    public int? PODR { get; set; }

    public string? CODE_MD { get; set; }

    public int? NPL { get; set; }

    //NAPR
    public string? _Naprs { get; internal set; }
    [NotMapped]
    public List<Napr2>? Naprs
    {
        get { return _Naprs == null ? null : JsonConvert.DeserializeObject<List<Napr2>>(_Naprs); }
        set { _Naprs = JsonConvert.SerializeObject(value); }
    }


    //MedDev
    private ICollection<MedDev>? medDevs;
    //public virtual ICollection<MedDev>? MedDevs
    //{
    //    get { return medDevs ?? (medDevs = new Collection<MedDev>()); }
    //    set { medDevs = value; }
    //}


    //MrUslN
    private ICollection<MrUslN>? mrUslNs;
    //public virtual ICollection<MrUslN>? MrUslNs
    //{
    //    get { return mrUslNs ?? (mrUslNs = new Collection<MrUslN>()); }
    //    set { mrUslNs = value; }
    //}

    // Случай к которому принадлежит данная услуга
    [Required]
    public int SluchId { get; set; }

    [ForeignKey("SluchId")]
    public virtual Sluch? Sluch { get; set; }

    //public string? MekErrors { get; internal set; }
    //[NotMapped]
    //public List<MekError>? _MekErrors
    //{
    //    get { return MekErrors == null ? null : JsonConvert.DeserializeObject<List<MekError>>(MekErrors); }
    //    set
    //    {
    //        MekErrors = value == null ? null : JsonConvert.SerializeObject(value);
    //    }
    //}


    //[NotMapped]
    //public string? Period
    //{
    //    get { return DATE_IN.ToString("MM.dd.yyyy") + " - " + DATE_OUT.ToString("MM.dd.yyyy"); }
    //}

    //[NotMapped]
    //public bool HasDATE_INErrors
    //{
    //    get { return HasMekErrorsForField("DATE_IN"); }
    //}

    //[NotMapped]
    //public bool HasDATE_OUTErrors
    //{
    //    get { return HasMekErrorsForField("DATE_OUT"); }
    //}

    //[NotMapped]
    //public bool HasIDSERVErrors
    //{
    //    get { return HasMekErrorsForField("IDSERV"); }
    //}

    //[NotMapped]
    //public bool HasPROFILErrors
    //{
    //    get { return HasMekErrorsForField("PROFIL"); }
    //}

    //[NotMapped]
    //public bool HasLPUErrors
    //{
    //    get { return HasMekErrorsForField("LPU"); }
    //}

    //[NotMapped]
    //public bool HasDETErrors
    //{
    //    get { return HasMekErrorsForField("DET"); }
    //}

    //[NotMapped]
    //public bool HasVID_VMEErrors
    //{
    //    get { return HasMekErrorsForField("VID_VME"); }
    //}

    //[NotMapped]
    //public bool HasDSErrors
    //{
    //    get { return HasMekErrorsForField("DS"); }
    //}

    //[NotMapped]
    //public bool HasErrors
    //{
    //    get { return MekErrors == null ? false : true; }
    //}


    //public bool HasMekErrorsForField(string field)
    //{
    //    if (HasErrors && _MekErrors != null)
    //        foreach (var err in _MekErrors) if (err.Field.Contains(field)) return true;

    //    return false;
    //}
}
