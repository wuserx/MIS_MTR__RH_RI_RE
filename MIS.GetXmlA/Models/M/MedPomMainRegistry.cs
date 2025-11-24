// Таблица пролеченных у нас
[Table("MedPomMainRegistry", Schema = "Spravoch")]
public class MedPomMainRegistry
{
    public int Id { get; set; }

    public string  FAM { get; set; }

    public string IM { get; set; }

    public string OT { get; set; }

    public DateTime BIRTHDAY { get; set; }

    public DateTime DATE_IN_CASE { get; set; }

    public DateTime DATEDOC { get; set; }


    public int? NSluch { get; set; }

    public string MOcod { get; set; }

    public string MO { get; set; }

    public string USL { get; set; }

    public int? USLcod { get; set; }

    public int? ProfCODE { get; set; }

    public string ProfName { get; set; }

    public string MkbCODE { get; set; }

    public string MkbName { get; set; }

    public string SMOcod { get; set; }

    public string SMO { get; set; }

    public string Disp { get; set; }

    public decimal? Sump { get; set; }
}