// Сведения о случае
[Table("Payment", Schema = "local")]
public class Payment
{
    public int Id { get; set; }

    [StringLength(15)]
    public string N_PLPR { get; set; }

    public DateTime  D_PLPR { get; set; }

    public string N_PLPR2 { get; set; }

    public DateTime? D_PLPR2 { get; set; }

    public string N_PLPR3 { get; set; }

    public DateTime? D_PLPR3 { get; set; }

    public int KOL_SH { get; set; }

    public int KOL_SCH { get; set; }

    public decimal  ITOG { get; set; }

    public string PRED { get; set; }

    // Получатель
    public string L_NAIM { get; set; }

    public string L_A { get; set; }

    public string L_B { get; set; }

    public string L_RS { get; set; }

    [StringLength(9)]
    public string L_BIC { get; set; }

    [StringLength(10)]
    public string L_IN { get; set; }

    [StringLength(20)]
    public string L_KP { get; set; }

    public string L_KB { get; set; }

    [StringLength(8)]
    public string L_OKTMO { get; set; }

    //
    public string T_NAIM { get; set; }

    public string T_A { get; set; }

    public string T_B { get; set; }

    public string T_RS { get; set; }

    [StringLength(9)]
    public string T_BIC { get; set; }

    [StringLength(10)]
    public string T_IN { get; set; }

    [StringLength(20)]
    public string T_KP { get; set; }

    [StringLength(8)]
    public string T_OKTMO { get; set; }

    public string FILENAME { get; set; }

    public string FILENAME_INN { get; set; }

    //N_SCH --- не буду использовать - буду дублировать payment
    public string _Schets { get; internal set; }
    [NotMapped]
    public List<SchetPayment> Schets
    {
        get { return _Schets == null ? null : JsonConvert.DeserializeObject<List<SchetPayment>>(_Schets); }
        set { _Schets = JsonConvert.SerializeObject(value); }
    }

    public string N_SCH { get; set; }

    public DateTime D_SCHET { get; set; }

    public string FILENAME_I { get; set; }

    public decimal SUM_SCH { get; set; }

    public decimal? SUM_SCH2 { get; set; }

    public decimal? SUM_SCH3 { get; set; }


    public int SL_SCH { get; set; }

    public bool OldVersion { get; set; }

    public bool? ManuallyEdited { get; set; }

    public int? SchetId { get; set; }
    [ForeignKey("SchetId")]
    public virtual Schet Schet { get; set; }

    [NotMapped]
    public bool HasManuallyEdited
    {
        get
        {
            return ManuallyEdited == true ? true : false;
        }
    }

}