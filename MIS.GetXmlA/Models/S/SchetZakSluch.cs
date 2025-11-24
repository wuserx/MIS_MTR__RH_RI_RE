[Table("SchetZakSluch", Schema = "Reestr")]
public class SchetZakSluch
{
    public int Id { get; set; }

    [Required]
    public string? FILENAME { get; set; }

    [Required]
    public decimal? SUMMAV { get; set; }   

    [Required]
    public decimal? SUMMAP { get; set; }

    public decimal? SANK_MEK { get; set; }

    public decimal? SUMMAV_PDF { get; set; }    
    public decimal? SUMMAV_SMP { get; set; }     
    public decimal? SUMMAV_FAP { get; set; }

    //public decimal? SUMMAV_SDU { get; set; }

    public decimal? SUMMAP_PDF { get; set; }
    public decimal? SUMMAP_SMP { get; set; }    
    public decimal? SUMMAP_FAP { get; set; }

    //public decimal? SUMMAP_SDU { get; set; }
}