// F010 Классификатор субъектов Российской Федерации
[Table("Region", Schema = "Spravoch")] //Не Федеральный
public class Region
{
    public int Id { get; set; }

    [Required]
    public string KOD_TF { get; set; }

        
    [Required]
    public string KOD_OKATO { get; set; }

    public string KOD_OGRN { get; set; }

    public string NameTf { get; set; }

    public string NameTfk { get; set; }

    public string Index { get; set; }

    public string Address { get; set; }

    public string FamDir { get; set; }

    public string ImDir { get; set; }

    public string OtDir { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Www { get; set; }

    public string ShefRp { get; set; }

    [Required]
    [StringLength(255)]
    public string SUBNAME { get; set; }

    [Required]
    public DateTime DATEBEG { get; set; }

    public DateTime? DATEEND { get; set; }

        //RegionAdditional

    public string NamRp { get; set; }

    public string NamDp { get; set; }

    public string NamIm { get; set; }

    public string NamTv { get; set; }

    [StringLength(10)]
    public string INN { get; set; }

    [StringLength(10)]
    public string BIK { get; set; }

    [StringLength(10)]
    public string Kpp { get; set; }

    [StringLength(20)]
    public string RS { get; set; }

    public string Bank { get; set; }

    public int? W { get; set; }

    public string Stroka1 { get; set; }

    public string Stroka2 { get; set; }

    public string Stroka3 { get; set; }
    //---

    public decimal? BZTSZ1 { get; set; }

    public decimal? BZTSZ2 { get; set; }

    private ICollection<Schet> schets;
    public virtual ICollection<Schet> Schets
    {
        get { return schets ?? (schets = new Collection<Schet>()); }
        set { schets = value; }
    }
}