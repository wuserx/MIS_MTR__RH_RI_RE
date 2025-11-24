[Table("Directive", Schema = "Local")]
public class Directive
{
    public int Id { get; set; }
    public DateTime  Date { get; set; }
    public int? Num { get; set; }
    public DateTime? DateDirective { get; set; }

    public string NumDirective { get; set; }
    public int? NumDirectiveInt { get; set; }

    public string FileName { get; set; }
    public DateTime DatePrevious { get; set; }
    public decimal?  Sum { get; set; }


    private ICollection<SchetDirective> schetDirectivs;
    public virtual ICollection<SchetDirective> SchetDirectivs
    {
        get { return schetDirectivs ?? (schetDirectivs = new Collection<SchetDirective>()); }
        set { schetDirectivs = value; }
    }

}
