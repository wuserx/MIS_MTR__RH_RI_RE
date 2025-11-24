// Сведения о случае

public class Sluch_mtr
{
    public int Id { get; set; }

    [StringLength(36)]
    public string? SL_ID { get; set; }

    public int? TYPE_AMB { get; set; }

    [NotMapped]
    public DateTime? DN_NEXT_DT { get; set; }

    [NotMapped]
    public int? REAB_SCORE { get; set; }

    [StringLength(12)]
    public string? VID_HMP { get; set; }

    public int? METOD_HMP { get; set; }

    [NotMapped]
    public int? IDMODP_HMP { get; set; }

    public int? PROFIL { get; set; }

    public int? PROFIL_K { get; set; }

    public int? DET { get; set; }

    [StringLength(3)]
    public string? P_CEL { get; set; }

    public int? DISP { get; set; }

    public DateTime? TAL_D { get; set; }

    [StringLength(50)]
    public string? NHISTORY { get; set; }

    
    public DateTime? DATE_1 { get; set; }

    
    public DateTime? DATE_2 { get; set; }

    public int? KD { get; set; }

    public decimal? WEI { get; set; }

    [StringLength(10)]
    public string? DS0 { get; set; }

    [StringLength(10)]
    public string? DS1 { get; set; }

    public int? DS1_PR { get; set; }
    public string? DS2s { get; internal set; }
    [NotMapped]
    public string[]? _Ds2
    {
        get { return DS2s == null ? null : JsonConvert.DeserializeObject<string[]>(DS2s); }
        set { DS2s = value == null ? null : JsonConvert.SerializeObject(value); }
    }

    public string? DS3s { get; internal set; }
    [NotMapped]
    public string[]? _Ds3
    {
        get { return DS3s == null ? null : JsonConvert.DeserializeObject<string[]>(DS3s); }
        set { DS3s = value == null ? null : JsonConvert.SerializeObject(value); }
    }

    public int? C_ZAB { get; set; }

    
    public int? DS_ONK { get; set; }

    public int? DN { get; set; }

    public string? CODE_MES1s { get; internal set; }
    [NotMapped]
    public string[]? CodeMes1
    {
        get { return CODE_MES1s == null ? null : JsonConvert.DeserializeObject<string[]>(CODE_MES1s); }
        set { CODE_MES1s = value == null ? null : JsonConvert.SerializeObject(value); }
    }

    [StringLength(20)]
    public string? CODE_MES2 { get; set; }

    public int? REAB { get; set; }

    public int? VK { get; set; }

    public int? PRVS { get; set; }

    [StringLength(4)]
    public string? VERS_SPEC { get; set; }

    public decimal? ED_COL { get; set; }

    public decimal? TARIF { get; set; }

    
    public decimal? SUM_M { get; set; }

    public decimal? SUM_M_OTKAZ_TER { get; set; }

    public int? HGR { get; set; }

    public string? LPU_1 { get; set; }

    public int? PODR { get; set; }

    public int? P_PER { get; set; }


    public string? IDDOKT { get; set; }

    public decimal? SUM_MPrimary { get; set; }

    public decimal? SUM_M_D { get; set; } //для формирования D - файла, если сумма частичная

    [StringLength(250)]
    public string? COMENTSL { get; set; }

    [StringLength(6)]
    public string? Kod_tarif { get; set; }


    public DateTime? Date_o { get; set; }

    public int? Yes { get; set; }

    public int? OnkTyp { get; set; }

    public string? TAL_NUM { get; set; }

    public DateTime? TAL_P { get; set; }

    public int? PR_D_N { get; set; }


    public bool? Surgical { get; set; }

    public bool? Thrombolytic { get; set; } //не нужен - в хирургическом

    public bool? ChemoTherapy { get; set; }

    public bool? RadiationTherapy { get; set; }

    public bool? ChemoRadiationTherapy { get; set; }

    public int? Diagnost { get; set; } //1 -КТ, 2-МРТ, 3 -УЗИ, 4 - Эндоскоп, 5 -МалеклДиаг, 6 - Гистолог


    
    public int? ZakSluchId { get; set; }

    //[ForeignKey("ZakSluchId")]
    //public virtual ZakSluch? ZakSluch { get; set; }

    //KSG_KPG
    //public int? KsgId { get; set; }
    //public virtual KsgKpg_mtr KsgKpg { get; set; }

    //OnkSl
    //public int? OnkSlId { get; set; }
    //public virtual OnkSl_mtr OnkSls { get; set; }


    private ICollection<DS2N>? ds2ns;
    //public virtual ICollection<DS2N>? Ds2ns
    //{
    //    get { return ds2ns ?? new Collection<DS2N>(); }
    //    set { ds2ns = value; }
    //}

    private ICollection<Naz>? nazs;
    public virtual ICollection<Naz>? Nazs
    {
        get { return nazs ?? (nazs = new Collection<Naz>()); }
        set { nazs = value; }
    }

    ////USL
    //private ICollection<Usl>? usls;
    //public virtual ICollection<Usl>? Usls
    //{
    //    get { return usls ?? (usls = new Collection<Usl>()); }
    //    set { usls = value; }
    //}

    ////LekPrSl
    //private ICollection<LekPrSl>? lekPrSls;
    //public virtual ICollection<LekPrSl>? LekPrSls
    //{
    //    get { return lekPrSls ?? (lekPrSls = new Collection<LekPrSl>()); }
    //    set { lekPrSls = value; }
    //}

    //Expertise
    private ICollection<Expertise_mtr>? expertises;
    //public virtual ICollection<Expertise_mtr>? Expertises
    //{
    //    get { return expertises ?? (expertises = new Collection<Expertise_mtr>()); }
    //    set { expertises = value; }
    //}

    private ICollection<Sank>? sanks;
    //public virtual ICollection<Sank>? Sanks
    //{
    //    get { return sanks ?? (sanks = new Collection<Sank>()); }
    //    set { sanks = value; }
    //}

    //NAPR
    public string? _Naprs { get; internal set; }
    [NotMapped]
    public List<Napr2>? Naprs
    {
        get { return _Naprs == null ? null : JsonConvert.DeserializeObject<List<Napr2>>(_Naprs); }
        set { _Naprs = JsonConvert.SerializeObject(value); }
    }


    //NApr
    private ICollection<NApr>? naprs;
    //public virtual ICollection<NApr>? NAprs
    //{
    //    get { return naprs ?? (naprs = new Collection<NApr>()); }
    //    set { naprs = value; }
    //}

    //CONS
    public string? _Conss { get; internal set; }
    [NotMapped]
    public List<Cons2>? Conss
    {
        get { return _Conss == null ? null : JsonConvert.DeserializeObject<List<Cons2>>(_Conss); }
        set { _Conss = JsonConvert.SerializeObject(value); }
    }


    //COns
    private ICollection<COns>? conss;
    //public virtual ICollection<COns>? COnss
    //{
    //    get { return conss ?? (conss = new Collection<COns>()); }
    //    set { conss = value; }
    //}



    public string? MekErrors { get; internal set; }
    [NotMapped]
    public List<MekError>? _MekErrors
    {
        get { return MekErrors == null ? null : JsonConvert.DeserializeObject<List<MekError>>(MekErrors); }
        set
        {
            MekErrors = value == null ? null : JsonConvert.SerializeObject(value);
        }
    }

    // привязка к уплаченным суммам
    private ICollection<SluchSum>? sluchSums;
    public virtual ICollection<SluchSum>? SluchSums
    {
        get { return sluchSums ?? (sluchSums = new Collection<SluchSum>()); }
        set { sluchSums = value; }
    }


    //// Это "парни" необходимы для отображения ошибок МЕК в детализациях.
    //// TODO: лучше вынести их в специальную обертку вокруг модели
    //[NotMapped]
    //public bool HasVID_HMPErrors
    //{
    //    get { return HasMekErrorsForField("VID_HMP"); }
    //}

    //[NotMapped]
    //public bool HasPROFILErrors
    //{
    //    get { return HasMekErrorsForField("PROFIL"); }
    //}

    //[NotMapped]
    //public bool HasPROFIL_KErrors
    //{
    //    get { return HasMekErrorsForField("PROFIL_K"); }
    //}

    //[NotMapped]
    //public bool HasDETErrors
    //{
    //    get { return HasMekErrorsForField("DET"); }
    //}

    //[NotMapped]
    //public bool HasP_CELErrors
    //{
    //    get { return HasMekErrorsForField("P_CEL"); }
    //}

    //[NotMapped]
    //public bool HasDISPErrors
    //{
    //    get { return HasMekErrorsForField("DISP"); }
    //}

    //[NotMapped]
    //public bool HasTAL_DErrors
    //{
    //    get { return HasMekErrorsForField("TAL_D"); }
    //}

    //[NotMapped]
    //public bool HasNHISTORYErrors
    //{
    //    get { return HasMekErrorsForField("NHISTORY"); }
    //}

    //[NotMapped]
    //public bool HasDS0Errors
    //{
    //    get { return HasMekErrorsForField("DS0"); }
    //}

    //[NotMapped]
    //public bool HasDS1Errors
    //{
    //    get { return HasMekErrorsForField("DS1"); }
    //}

    //[NotMapped]
    //public bool HasDS2Errors
    //{
    //    get { return HasMekErrorsForField("DS2"); }
    //}

    //[NotMapped]
    //public bool HasDS3Errors
    //{
    //    get { return HasMekErrorsForField("DS3"); }
    //}

    //[NotMapped]
    //public bool HasDBErrors
    //{
    //    get { return HasMekErrorsForField("DB"); }
    //}

    //[NotMapped]
    //public bool HasCODE_MES1Errors
    //{
    //    get { return HasMekErrorsForField("CODE_MES1"); }
    //}



    //[NotMapped]
    //public bool HasCODE_MES2Errors
    //{
    //    get { return HasMekErrorsForField("CODE_MES2"); }
    //}

    //[NotMapped]
    //public bool HasREABErrors
    //{
    //    get { return HasMekErrorsForField("REAB"); }
    //}

    //[NotMapped]
    //public bool HasPRVSErrors
    //{
    //    get { return HasMekErrorsForField("PRVS"); }
    //}

    //[NotMapped]
    //public bool HasVERS_SPECErrors
    //{
    //    get { return HasMekErrorsForField("VERS_SPEC"); }
    //}

    //[NotMapped]
    //public bool HasED_COLErrors
    //{
    //    get { return HasMekErrorsForField("ED_COL"); }
    //}

    //[NotMapped]
    //public bool HasTARIFErrors
    //{
    //    get { return HasMekErrorsForField("TARIF"); }
    //}

    //[NotMapped]
    //public bool HasSUM_MErrors
    //{
    //    get { return HasMekErrorsForField("SUM_M"); }
    //}

    //[NotMapped]
    //public bool HasCOMENTSLErrors
    //{
    //    get { return HasMekErrorsForField("COMENTSL"); }
    //}

    //[NotMapped]
    //public bool HasMETOD_HMPErrors
    //{
    //    get { return HasMekErrorsForField("METOD_HMP"); }
    //}

    //[NotMapped]
    //public bool HasC_ZABrrors
    //{
    //    get { return HasMekErrorsForField("C_ZAB"); }
    //}


    //[NotMapped]
    //public bool HasErrors
    //{
    //    get { return Sanks.Any() && MekErrors != null; }
    //}

    //[NotMapped]
    //public bool HasUslErrors
    //{
    //    get
    //    {
    //        foreach (var usl in Usls)
    //            if (usl.MekErrors != null) return true;

    //        return false;
    //    }
    //}

    //public bool HasMekErrorsForField(string field)
    //{
    //    if (HasErrors)
    //        foreach (var err in _MekErrors) if (err.Field.Contains(field)) return true;

    //    return false;
    //}

    //[NotMapped]
    //public string? MekErrorCodes
    //{
    //    get { return String.Join(",", Sanks.Select(s => s.S_OSN.ToString()).Distinct()); }
    //}

    //[NotMapped]
    //public string? ExpertisesCodes
    //{
    //    get { return String.Join(",", Expertises.Select(s => s.IDVID.ToString()).Distinct()); }
    //}


    //[NotMapped]
    //public string? ExpertisesErrors
    //{
    //    get { return String.Join(",", Expertises.Where(e => e.KodOtk != null && e.KodOtk != 0).Select(s => s.KodOtk.ToString()).Distinct()); }
    //}

}
