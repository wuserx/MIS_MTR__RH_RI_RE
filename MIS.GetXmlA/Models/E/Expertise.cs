
public class Expertise
{
    public int Id { get; set; }

    public int TopicExpertiseId { get; set; } //Какой экспертизе принадлежит
    public bool Main { get; set; } //основная экспертиза
    public int usl_ok { get; set; }
    public int TypeExpertise { get; set; } //110 - МЭЭ целевая (постоянная) 121 - МЭЭ плановая (случ) 122 - МЭЭ плановая (тематическая)    
                                            //210 - ЭКМП целевая (постоянная) 221 - ЭКМП плановая (случ) 222 - ЭКМП плановая (тематическая)   
    public int IDVID { get; set; } //F006 Классификатор видов контроля (VidExp)
    public int? KodOtk { get; set; } //F014 Классификатор причин отказа в оплате медицинской помощи (OplOtk)

    public string Comment { get; set; }
    public DateTime DCreateExpertise { get; set; }
    public DateTime? DExpertise { get; set; }

    public DateTime? DAktExpertiseMee { get; set; }

    public DateTime? DAktExpertiseEkmp { get; set; }

    public decimal? SumExpertise { get; set; }
    public decimal? SumExpertisePenalty { get; set; }
    public decimal? KoefNO { get; set; }
    public decimal? KoefSS { get; set; }

    //public string _SluchDblId { get; internal set; }
    //[NotMapped]
    //public int[] SluchDblId
    //{
    //    get { return _SluchDblId == null ? null : JsonConvert.DeserializeObject<int[]>(_SluchDblId); }
    //    set { _SluchDblId = value == null ? null : JsonConvert.SerializeObject(value); }
    //}


    //public string _SluchPuomps { get; internal set; }
    //[NotMapped]
    //public IEnumerable<MedPomMainRegistry> SluchPuomps
    //{
    //    get { return _SluchPuomps == null ? null : JsonConvert.DeserializeObject<IEnumerable<MedPomMainRegistry>>(_SluchPuomps); }
    //    set { _SluchPuomps = JsonConvert.SerializeObject(value); }
    //}


    //public int SluchId { get; set; }
    //[ForeignKey("SluchId")]
    //public virtual Sluch Sluch { get; set; }

    public int ExpertiseBoxId { get; set; }
       

}
