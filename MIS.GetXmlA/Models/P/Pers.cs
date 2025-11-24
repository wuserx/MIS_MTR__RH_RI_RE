public class Pers
{
    public string? ID_PAC { get; set; }
    public string? FAM { get; set; }
    public string? IM { get; set; }
    public string? OT { get; set; }
    public int? W { get; set; }
    public DateTime? DR { get; set; }
    public string? TEL { get; set; }
    public string? FAM_P { get; set; }
    public string? IM_P { get; set; }
    public string? OT_P { get; set; }
    public int? W_P { get; set; }
    public DateTime? DR_P { get; set; }
    public string? MR { get; set; }
    public string? DOCTYPE { get; set; }
    public string? DOCSER { get; set; }
    public string? DOCNUM { get; set; }
    public DateTime? DOCDATE { get; set; }
    public string? DOCORG { get; set; }
    public string? SNILS { get; set; }
    public string? OKATOG { get; set; }
    public string? OKATOP { get; set; }
    public string? COMENTP { get; set; }


    public string? DOSTs { get; internal set; }
    public int[]? Dosts
    {
        get { return DOSTs == null ? null : JsonConvert.DeserializeObject<int[]>(DOSTs); }
        set { DOSTs = value == null ? null : JsonConvert.SerializeObject(value); }
    }


    public string? DOST_Ps { get; internal set; }
    public int[]? DostPs
    {
        get { return DOST_Ps == null ? null : JsonConvert.DeserializeObject<int[]>(DOST_Ps); }
        set { DOST_Ps = value == null ? null : JsonConvert.SerializeObject(value); }
    }
}
