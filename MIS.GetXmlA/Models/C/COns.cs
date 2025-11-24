[Table("COns", Schema = "Reestr")]
public class COns
{
    public int Id { get; set; }
    public int? PR_CONS { get; set; }
    public DateTime? DT_CONS { get; set; }

    [Required]
    public int SluchId { get; set; }
    [ForeignKey("SluchId")]
    public virtual Sluch? Sluch { get; set; }

}