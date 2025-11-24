public class OnkSl_mtr
{
    public int Id { get; set; }

    public int? DS1_T { get; set; }
      
    public int? STAD { get; set; }
      
    public int? ONK_T { get; set; }
        
    public int? ONK_N { get; set; }
      
    public int? ONK_M { get; set; }

    public int? MTSTZ { get; set; }

    [Required]
    public int SluchId { get; set; }
    //public virtual Sluch_mtr? Sluch { get; set; }

    // B_DIAG
    //public string? _Bdiags { get; internal set; }
    //[NotMapped]
    //public List<Bdiag2>? Bdiags
    //{
    //    get { return _Bdiags == null ? null : JsonConvert.DeserializeObject<List<Bdiag2>>(_Bdiags); }
    //    set { _Bdiags = JsonConvert.SerializeObject(value); }
    //}

    //B_PROT
    //public string? _Bprots { get; internal set; }
    //[NotMapped]
    //public List<Bprot2>? Bprots
    //{
    //    get { return _Bprots == null ? null : JsonConvert.DeserializeObject<List<Bprot2>>(_Bprots); }
    //    set { _Bprots = JsonConvert.SerializeObject(value); }
    //}


    public decimal? SOD { get; set; }

    public int? K_FR { get; set; }

    public decimal? WEI { get; set; }

    public int? HEI { get; set; }

    public decimal? BSA { get; set; }

    //ONK_USL
    //private ICollection<OnkUsl>? onkusls;
    //public virtual ICollection<OnkUsl>? OnkUsls
    //{
    //    get { return onkusls ?? (onkusls = new Collection<OnkUsl>()); }
    //    set { onkusls = value; }
    //}

    //BDiag
    //private ICollection<BDiag>? bdiags;
    //public virtual ICollection<BDiag>? BDiags
    //{
    //    get { return bdiags ?? (bdiags = new Collection<BDiag>()); }
    //    set { bdiags = value; }
    //}

    //BProt
    //private ICollection<BProt>? bprots;
    //public virtual ICollection<BProt>? BProts
    //{
    //    get { return bprots ?? (bprots = new Collection<BProt>()); }
    //    set { bprots = value; }
    //}

}
