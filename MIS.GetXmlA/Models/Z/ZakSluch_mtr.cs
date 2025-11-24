// Сведения о законченном случае

public class ZakSluch_mtr
{
    public int Id { get; set; }
    public string? FILENAME { get; set; }
    public string? DISP { get; set; }

    public string? SOC { get; set; }

    public int? FIN_TYPE { get; set; }

    [Required]
    public int? N_ZAP { get; set; }

    public int? PR_NOV { get; set; }
    [Required]
    public int? IDCASE { get; set; }

    public long? IDCASEL { get; set; }

    [Required]
    public int USL_OK { get; set; }

    [Required]
    public int? VIDPOM { get; set; }

        
    public int? FOR_POM { get; set; }

    [StringLength(6)]
    public string? NPR_MO { get; set; }

    public DateTime? NPR_DATE { get; set; }

    [NotMapped]
    public string? NPR_NUMBER { get; set; }

    public int? P_DISP2 { get; set; }

    [Required, StringLength(6)]
    public string? LPU { get; set; }

    [Required]
    public DateTime? DATE_Z_1 { get; set; }

    [Required]
    public DateTime? DATE_Z_2 { get; set; }

    public int? KD_Z { get; set; }

    public string? VNOV_Ms { get; internal set; }
    [NotMapped]
    public int[]? _VnovMs
    {
        get { return VNOV_Ms == null ? null : JsonConvert.DeserializeObject<int[]>(VNOV_Ms); }
        set { VNOV_Ms = JsonConvert.SerializeObject(value); }
    }

        
    public int? RSLT { get; set; }

    public int? ISHOD { get; set; }

    public string? OS_SLUCHs { get; internal set; }
    [NotMapped]
    public int[]? _OsSluchs
    {
        get { return OS_SLUCHs == null ? null : JsonConvert.DeserializeObject<int[]>(OS_SLUCHs); }
        set { OS_SLUCHs = value == null ? null : JsonConvert.SerializeObject(value); }
    }
        

    public int? VB_P { get; set; }

    [NotMapped]
    public long? VB_P_IDCASE { get; set; }

    [NotMapped]
    public string? VB_P_NSCHET { get; set; }

    [Required]
    public int? IDSP { get; set; }

    [Required]
    public decimal? SUMV { get; set; }

    public int OPLATA { get; set; }

    public decimal? SUMP { get; set; }

    public decimal? SANK_IT { get; set; }


    public int? VBR { get; set; }
    public int? P_OTK { get; set; }
    public int? RSLT_D { get; set; }
    public int? DS1_PR { get; set; }
    public int? PR_D_N { get; set; }

    public decimal? SUMVPrimary { get; set; }

    public decimal? SUMPPrimary { get; set; }

    public decimal? SANK_ITPrimary { get; set; }

    public int? IncludeD { get; set; } //Если сумма стоит то законченный случай участвует в D-файле

    // Cчет к которому принадлежит данный законченный случай
    [Required]
    public int SchetId { get; set; }
    public virtual Schet? Schet { get; set; }

    public int? SchetIdLocal { get; set; }
    public int? Yes { get; set; }

    private ICollection<Sluch>? sluchs;
    public virtual ICollection<Sluch>? Sluchs
    {
        get { return sluchs ?? (sluchs = new Collection<Sluch>()); }
        set { sluchs = value; }
    }

    //public int? ZakSluchIdParent { get; set; }

    // Сведения о пациенте
    [Required]
    public int? VPOLIS { get; set; }

    [StringLength(10)]
    public string? SPOLIS { get; set; }

    [StringLength(20)]
    public string? NPOLIS { get; set; }

    [StringLength(16)]
    public string? ENP { get; set; }

    [StringLength(5)]
    public string? ST_OKATO { get; set; }

    public string? SMO { get; set; }

    public int? INV { get; set; }

    public int? MSE { get; set; }

    public string? SMO_OGRN { get; set; }

    public string? SMO_OK { get; set; }

    public string? SMO_NAM { get; set; }

    public string? ID_PAC { get; set; }

    [StringLength(40)]
    public string? FAM { get; set; }

    [StringLength(40)]
    public string? IM { get; set; }

    [StringLength(40)]
    public string? OT { get; set; }

    [Required]
    public int? W { get; set; }

    public string? TEL { get; set; }

    [Required]
    public DateTime? DR { get; set; }

    [StringLength(40)]
    public string? FAM_P { get; set; }

    [StringLength(40)]
    public string? IM_P { get; set; }

    [StringLength(40)]
    public string? OT_P { get; set; }

    public int? W_P { get; set; }

    public DateTime? DR_P { get; set; }

    [StringLength(100)]
    public string? MR { get; set; }

    [StringLength(2)]
    public string? DOCTYPE { get; set; }

    [StringLength(10)]
    public string? DOCSER { get; set; }

    [StringLength(20)]
    public string? DOCNUM { get; set; }

    public DateTime? DOCDATE { get; set; }

    public string? DOCORG { get; set; }

    [StringLength(14)]
    public string? SNILS { get; set; }

    [StringLength(11)]
    public string? OKATOG { get; set; }

    [StringLength(11)]
    public string? OKATOP { get; set; }

    [Required, StringLength(9)]
    public string? NOVOR { get; set; }

    public int? VNOV_D { get; set; }

    [StringLength(250)]
    public string? COMMENTP { get; set; }

    public int? VedPri { get; set; }

    public DateTime? Ds { get; set; }

    public int? S_TIP { get; set; } // F006

    public string?  NUM_AKT { get; set; }

    public DateTime? DATE_AKT { get; set; }

    [NotMapped]
    public bool? IS_NOT_SHOW_PORTAL_LK { get; set; }


    [NotMapped]
    public string? sluchsDsList
    {
        get
        {
            return string.Join(", ", Sluchs.Select(s => s.DS1));
        }
    }

    [NotMapped]
    public string? sluchsProfilList
    {
        get
        {
            return string.Join(", ", Sluchs.Select(s => s.PROFIL));
        }
    }

    [NotMapped]
    public string? pcelList
    {
        get
        {
            return string.Join(", ", Sluchs.Select(s => s.P_CEL));
        }
    }



    [NotMapped]
    public string? ST_OKATO29
    {
        get { return (COMMENTP == null || COMMENTP.Length <= 6) ? "Нет в ЕРЗ" : COMMENTP.Substring(COMMENTP.IndexOf("okato:") + 6, 5); }
        //get { return ""; }
    }

    [NotMapped]
    public bool HasNoOkatoError
    {
        get { return (COMMENTP == null || COMMENTP.Length <=6 ) ? true : ST_OKATO != COMMENTP.Substring(COMMENTP.IndexOf("okato:") + 6, 5); }
        // get { return true; }
    }


    [NotMapped]
    public string? Period
    {
        get { return DATE_Z_1?.ToString("MM.dd.yyyy") + " - " + DATE_Z_2?.ToString("MM.dd.yyyy"); }
    }

    public string? DOSTs { get; internal set; }
    [NotMapped]
    public int[]? Dosts
    {
        get { return DOSTs == null ? null : JsonConvert.DeserializeObject<int[]>(DOSTs); }
        set { DOSTs = value == null ? null : JsonConvert.SerializeObject(value); }
    }
        

    public string? DOST_Ps { get; internal set; }
    [NotMapped]
    public int[]? DostPs
    {
        get { return DOST_Ps == null ? null : JsonConvert.DeserializeObject<int[]>(DOST_Ps); }
        set { DOST_Ps = value == null ? null : JsonConvert.SerializeObject(value); }
    }

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

    private ICollection<SankZ_mtr>? sankzs;
    //public virtual ICollection<SankZ_mtr>? SankZs
    //{
    //    get { return sankzs ?? (sankzs = new Collection<SankZ_mtr>()); }
    //    set { sankzs = value; }
    //}

    [NotMapped]
    public bool HasDBLSchetRegistry
    {
        get
        {
            foreach (var sluch in this.Sluchs)
                if (sluch.MekErrors != null)
                    foreach (var err in sluch._MekErrors) if (err.CodeTfoms.Contains("sl0105")) return true;

            return false;
        }
    }

    //[NotMapped]
    //public bool HasNoSrzPatientError
    //{
    //    get
    //    {
    //        if (HasErrors && MekErrors != null)
    //            foreach (var err in _MekErrors) if (err.CodeTfoms.Contains("zs0110")) return true;
    //        return false;
    //    }
    //}

    //[NotMapped]
    //public bool HasDBLMainRegistry
    //{
    //    get
    //    {
    //        if (HasErrors && MekErrors != null)
    //            foreach (var err in _MekErrors) if (err.CodeTfoms.Contains("zs0111")) return true;
    //        return false;
    //    }
    //}

    //HasErrorsExpertise

    [NotMapped]
    public bool HasDeathError
    {
        get
        {
            string DeathStr = " 105 , 106 , 205 , 206 , 405 , 406 , 411 ";
            if (DeathStr.IndexOf(RSLT.ToString()) > 0   ) return true;
            return false;
        }
    }

    [NotMapped]
    public bool HasDeathLocalError
    {
        get
        {
            if (MekErrors != null)
                foreach (var err in _MekErrors) if (err.CodeTfoms.Contains("zs0115")) return true;
            return false;
        }
    }

    //[NotMapped]
    //public bool HasIDCASEErrors
    //{
    //    get { return HasMekErrorsForField("IDCASE"); }
    //}

    //[NotMapped]
    //public bool HasUSL_OKErrors
    //{
    //    get { return HasMekErrorsForField("USL_OK"); }
    //}

    //[NotMapped]
    //public bool HasVIDPOMErrors
    //{
    //    get { return HasMekErrorsForField("VIDPOM"); }
    //}

    //[NotMapped]
    //public bool HasFOR_POMErrors
    //{
    //    get { return HasMekErrorsForField("FOR_POM"); }
    //}

    //[NotMapped]
    //public bool HasNRP_MOErrors
    //{
    //    get { return HasMekErrorsForField("NRP_MO"); }
    //}

    //[NotMapped]
    //public bool HasNRP_DATEErrors
    //{
    //    get { return HasMekErrorsForField("NRP_DATE"); }
    //}


    //[NotMapped]
    //public bool HasP_DISP2Errors
    //{
    //    get { return HasMekErrorsForField("P_DISP2"); }
    //}

    //[NotMapped]
    //public bool HasVNOV_MErrors
    //{
    //    get { return HasMekErrorsForField("VNOV_M"); }
    //}

    //[NotMapped]
    //public bool HasLPUErrors
    //{
    //    get { return HasMekErrorsForField("LPU"); }
    //}

    //[NotMapped]
    //public bool HasDATE_Z_1Errors
    //{
    //    get { return HasMekErrorsForField("DATE_Z_1"); }
    //}

    //[NotMapped]
    //public bool HasDATE_Z_2Errors
    //{
    //    get { return HasMekErrorsForField("DATE_Z_2"); }
    //}

    //[NotMapped]
    //public bool HasKD_ZErrors
    //{
    //    get { return HasMekErrorsForField("KD_Z"); }
    //}

    //[NotMapped]
    //public bool HasRSLTErrors
    //{
    //    get { return HasMekErrorsForField("RSLT"); }
    //}

    //[NotMapped]
    //public bool HasISHODErrors
    //{
    //    get { return HasMekErrorsForField("ISHOD"); }
    //}


    //[NotMapped]
    //public bool HasOS_SLUCHErrors
    //{
    //    get { return HasMekErrorsForField("OS_SLUCH"); }
    //}

    //[NotMapped]
    //public bool HasV_BPErrors
    //{
    //    get { return HasMekErrorsForField("V_BP"); }
    //}

    //[NotMapped]
    //public bool HasIDSPErrors
    //{
    //    get { return HasMekErrorsForField("IDSP"); }
    //}


    //[NotMapped]
    //public bool HasSUMPErrors
    //{
    //    get { return HasMekErrorsForField("SUMP"); }
    //}

    //[NotMapped]
    //public bool HasOPLATAErrors
    //{
    //    get { return HasMekErrorsForField("OPLATA"); }
    //}

    //[NotMapped]
    //public bool HasSUMVErrors
    //{
    //    get { return HasMekErrorsForField("SUMV"); }
    //}

    //[NotMapped]
    //public bool HasSANK_ITErrors
    //{
    //    get { return HasMekErrorsForField("SANK_IT"); }
    //}

    //[NotMapped]
    //public bool HasNPOLISErrors
    //{
    //    get { return HasMekErrorsForField("NPOLIS"); }
    //}

    //[NotMapped]
    //public bool HasVPOLISErrors
    //{
    //    get { return HasMekErrorsForField("VPOLIS"); }
    //}

    //[NotMapped]
    //public bool HasSPOLISErrors
    //{
    //    get { return HasMekErrorsForField("SPOLIS"); }
    //}

    //[NotMapped]
    //public bool HasENPErrors
    //{
    //    get { return HasMekErrorsForField("ENP"); }
    //}

    //[NotMapped]
    //public bool HasST_OKATOErrors
    //{
    //    get { return HasMekErrorsForField("ST_OKATO"); }
    //}

    //[NotMapped]
    //public bool HasFAMErrors
    //{
    //    get { return HasMekErrorsForField("FAM"); }
    //}

    //[NotMapped]
    //public bool HasIMErrors
    //{
    //    get { return HasMekErrorsForField("IM"); }
    //}

    //[NotMapped]
    //public bool HasOTErrors
    //{
    //    get { return HasMekErrorsForField("OT"); }
    //}

    //[NotMapped]
    //public bool HasWErrors
    //{
    //    get { return HasMekErrorsForField("W"); }
    //}

    //[NotMapped]
    //public bool HasDRErrors
    //{
    //    get { return HasMekErrorsForField("DR"); }
    //}


    //[NotMapped]
    //public bool HasFAM_PErrors
    //{
    //    get { return HasMekErrorsForField("FAM_P"); }
    //}

    //[NotMapped]
    //public bool HasIM_PErrors
    //{
    //    get { return HasMekErrorsForField("IM_P"); }
    //}

    //[NotMapped]
    //public bool HasOT_PErrors
    //{
    //    get { return HasMekErrorsForField("OT_P"); }
    //}

    //[NotMapped]
    //public bool HasW_PErrors
    //{
    //    get { return HasMekErrorsForField("W_P"); }
    //}

    //[NotMapped]
    //public bool HasDR_PErrors
    //{
    //    get { return HasMekErrorsForField("DR_P"); }
    //}

    //[NotMapped]
    //public bool HasMRErrors
    //{
    //    get { return HasMekErrorsForField("MR"); }
    //}

    //[NotMapped]
    //public bool HasDOCTYPErrors
    //{
    //    get { return HasMekErrorsForField("DOCTYPE"); }
    //}

    //[NotMapped]
    //public bool HasDOCSERErrors
    //{
    //    get { return HasMekErrorsForField("DOCSER"); }
    //}

    //[NotMapped]
    //public bool HasDOCNUMErrors
    //{
    //    get { return HasMekErrorsForField("DOCNUM"); }
    //}

    //[NotMapped]
    //public bool HasSNILSErrors
    //{
    //    get { return HasMekErrorsForField("SNILS"); }
    //}

    //[NotMapped]
    //public bool HasOKATOGErrors
    //{
    //    get { return HasMekErrorsForField("OKATOG"); }
    //}

    //[NotMapped]
    //public bool HasOKATOPErrors
    //{
    //    get { return HasMekErrorsForField("OKATOP"); }
    //}

    //[NotMapped]
    //public bool HasNOVORErrors
    //{
    //    get { return HasMekErrorsForField("NOVOR"); }
    //}

    //[NotMapped]
    //public bool HasVNOV_DErrors
    //{
    //    get { return HasMekErrorsForField("VNOV_D"); }
    //}

    //[NotMapped]
    //public bool HasCOMMENTPErrors
    //{
    //    get { return HasMekErrorsForField("COMMENTP"); }
    //}

    //[NotMapped]
    //public bool HasErrors
    //{
    //    get
    //    {
    //        return Sluchs.Where(s => s.Sanks.Any()).Any() || this.SankZs.Any();
    //    }
    //}


    //public bool HasMekErrorsForField(string field)
    //{
    //    if (HasErrors && MekErrors != null)
    //        foreach (var err in _MekErrors) if (err.Field.Contains(field)) return true;
    //    return false;
    //}

    //[NotMapped]
    //public string? MekErrorCodes
    //{
    //    get { return Schet.VERSION == "3.0" ?
    //            String.Join(" | ", Sluchs.Select(s => s.MekErrorCodes).Distinct()) :
    //            String.Join(" | ", SankZs.Select(s => s.S_OSN).Distinct())
    //            ; }
    //}

    [NotMapped]
    public string? Type
    {
        get
        {
            return this.FILENAME.ToString().Substring(0, this.FILENAME.ToString().IndexOf("0"));
        }
    }

    [NotMapped]
    public bool HasExpertiseAkt // Если даты ата стоят, то МЭЭ или ЭКМП были проведены по этому случаю
    {
        get
        {
            return Sluchs.Where(s => s.Expertises.Where(e => e.DAktExpertiseEkmp != null || e.DAktExpertiseMee != null).Any()).Any();
        }
    }


    [NotMapped]
    public bool HasExpertiseMEE
    {
        get
        {
            return Sluchs.Where(s => s.Expertises.Where(e => (int) e.TypeExpertise/100 == 1).Any()).Any();
        }
    }

    [NotMapped]
    public bool HasExpertiseErrMEE
    {
        get
        {
            return Sluchs.Where(s => s.Expertises.Where(e =>e.KodOtk > 0 ).Where(e => (int)e.TypeExpertise / 100 == 1).Any()).Any();
        }
    }


    [NotMapped]
    public bool HasExpertiseEKMP
    {
        get
        {
            return Sluchs.Where(s => s.Expertises.Where(e => (int)e.TypeExpertise / 100 == 2).Any()).Any();
        }
    }

    [NotMapped]
    public bool HasExpertiseErrEKMP
    {
        get
        {
            return Sluchs.Where(s => s.Expertises.Where(e => e.KodOtk > 0).Where(e => (int)e.TypeExpertise / 100 == 2).Any()).Any();
        }
    }

    //[NotMapped]
    //public string? ExpertiseCodes
    //{
    //    get
    //    {
    //        return 
    //            String.Join(" | ", Sluchs.Select(s => s.ExpertisesCodes).Distinct());
    //    }
    //}

    //[NotMapped]
    //public string? ExpertiseErrors
    //{
    //    get
    //    {
    //        return
    //            String.Join(" | ", Sluchs.Select(s => s.ExpertisesErrors).Distinct());
    //    }
    //}

    [NotMapped]
    public bool HasDFile
    {
        get { return IncludeD == 1 ? true : false; }
    }

    [NotMapped]
    public bool HasDs
    {
        get { return Ds != null ? true : false; }
    }

}
