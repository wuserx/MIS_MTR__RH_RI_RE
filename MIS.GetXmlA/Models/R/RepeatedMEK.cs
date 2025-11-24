
[Table("RepeatedMEK", Schema = "Reestr")] //Не Федеральный
public class RepeatedMEK
{
    public int Id { get; set; }
    public int? ZakSluchId { get; set; }
    public int? OPLATA { get; set; }
    public decimal? S_SUM { get; set; }
    public string? S_CODE { get; set; }
    public int? S_TIP { get; set; }
    public int? S_OSN { get; set; }
    public DateTime? DATE_ACT { get; set; }
    public string? NUM_ACT { get; set; }
    public string? S_COM { get; set; }
    public int? S_IST { get; set; }
}


