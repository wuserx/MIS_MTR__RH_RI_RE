

//[Table("Schet", Schema = "Reestr")]
public class Schet
{
    private Schet() { }

    [Key]
    public int Id { get; set; }

    public string? DISP { get; set; }
    public int YEAR_REPORT { get; set; }
    public int MONTH_REPORT { get; set; }
    public decimal? SUMMAV_PDF { get; set; }
    public decimal? SUMMAP_PDF { get; set; }
    public decimal? SUMMAV_SMP { get; set; }
    public decimal? SUMMAP_SMP { get; set; }
    public decimal? SUMMAV_SDU { get; set; }
    public decimal? SUMMAP_SDU { get; set; }
    public decimal? SUMMAV_FAP { get; set; }
    public decimal? SUMMAP_FAP { get; set; }

    public int? SD_Z { get; set; }
    
    public string? FILENAME { get; set; }    
    
    public string? FILENAME_PACKET { get; set; }
    
    public DateTime? DATA { get; set; }
    
    public string? C_OKATO1 { get; set; }
    
    public int? CODE { get; set; }
    
    public int? YEAR { get; set; }
    
    public int? MONTH { get; set; }

    [Required, StringLength(15)]
    public string? NSCHET { get; set; }
    
    public DateTime? DSCHET { get; set; }

    public string? PLAT { get; set; }

    public DateTime? DSCHET_P { get; set; }

    public DateTime? DSCHET_O { get; set; }

    public DateTime? DSCHET_D { get; set; }
    
    public decimal? SUMMAV { get; set; }

    [StringLength(250)]
    public string? COMENTS { get; set; }
    
    public decimal? SUMMAP { get; set; }

    public decimal? SANK_MEK { get; set; }

    public decimal? SANK_MEE { get; set; }

    public decimal? SANK_EKMP { get; set; }

    public int? RegionId { get; set; }

    [ForeignKey("RegionId")]
    public virtual Region? Region { get; set; }

    [StringLength(6)]
    public string? CODE_MO { get; set; }

    public int? LOCK { get; set; }

    public int? AFileN { get; set; }

    public int? LocalResult { get; set; }

    public decimal? SUMMAVPrimary { get; set; }

    public decimal? SUMMAPPrimary { get; set; }

    public decimal? SANK_MEKPrimary { get; set; }

    public decimal? SANK_MEEPrimary { get; set; }

    public decimal? SANK_EKMPPrimary { get; set; }  

    public string? VERSION { get; set; }

    public int? DirectiveId { get; set; } //один к одному не подходит        

    public string? DirectiveString { get; set; }


    //private ICollection<Directive> directives;
    //public virtual ICollection<Directive> Directives
    //{
    //    get { return directives ?? (directives = new Collection<Directive>()); }
    //    set { directives = value; }
    //}

    //private ICollection<SchetDirective>? schetDirectivs;
    //public virtual ICollection<SchetDirective>? SchetDirectivs
    //{
    //    get { return schetDirectivs ?? (schetDirectivs = new Collection<SchetDirective>()); }
    //    set { schetDirectivs = value; }
    //}



    public bool? Flk { get; set; }

    //private ICollection<Payment>? payments;
    //public virtual ICollection<Payment>? Payments
    //{
    //    get { return payments ?? (payments = new Collection<Payment>()); }
    //    set { payments = value; }
    //}



    //public string? AFileCounters { get; internal set; }
    //[NotMapped]
    //public int[]? _AFileCounters
    //{
    //    get { return AFileCounters == null ? null : JsonConvert.DeserializeObject<int[]>(AFileCounters); }
    //    set
    //    {
    //        AFileCounters = JsonConvert.SerializeObject(value);
    //    }
    //}

    //private ICollection<ZakSluch>? zakSluchs;
    //public virtual ICollection<ZakSluch>? ZakSluchs
    //{
    //    get { return zakSluchs ?? (zakSluchs = new Collection<ZakSluch>()); }
    //    set { zakSluchs = value; }
    //}

    //[NotMapped]
    //public bool IsTypeOfD
    //{
    //    get
    //    {
    //        return FILENAME[0] == 'D';
    //    }
    //}

    //[NotMapped]
    //public bool IsClock
    //{
    //    get
    //    {
    //        return LOCK == 1;
    //    }
    //}

    //[NotMapped]
    //public bool IsPayment
    //{
    //    get
    //    {
    //        return Payments.Count>0 ;
    //    }
    //}
    

    //[NotMapped]
    //public bool? HasDirective
    //{
    //    get
    //    {
    //        decimal? summ = null;
    //        summ = SchetDirectivs.Count()>0 ? (decimal?)SchetDirectivs.Sum(s => s.SummaDirective) : (decimal?)null;

    //        if (SUMMAP == summ && summ != null) return true;

    //        if (SUMMAP != summ && summ > 0) return false;

    //        if (summ == 0 || summ == null) return null;

    //        return null;

    //    }
    //}
}
