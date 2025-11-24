[Table("SluchSum", Schema = "Reestr")]
public class SluchSum
{
    public int Id { get; set; }
    public int PaymentId { get; set; }
    public int ScluchId { get; set; }
    public decimal SUM_SCH { get; set; }
    public string N_PLPR { get; set; }
    public DateTime D_PLPR  { get; set; }
    public bool? FirstSum { get; set; }

    [ForeignKey("ScluchId")]  //тут ошибся но базу трогать не хочется поэтому в запросах  надо ставить не SluchId, а ScluchId
    public virtual Sluch Sluch { get; set; }
}