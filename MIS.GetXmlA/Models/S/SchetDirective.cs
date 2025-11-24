[Table("SchetDirective", Schema = "Reestr")]
public class SchetDirective
{
    public int Id { get; set; }

       
    [Required]
    public int SchetId { get; set; }
    [ForeignKey("SchetId")]
    public virtual Schet  Schet { get; set; }


    [Required]
    public int DirectiveId { get; set; }
    [ForeignKey("DirectiveId")]
    public virtual Directive Directive { get; set; }

    public decimal  SummaDirective { get; set; }

}
