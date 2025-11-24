public class SANK_RI_RE
{
    private readonly IEnumerable<Sluch_mtr> sluchs;

    public SANK_RI_RE(IEnumerable<Sluch_mtr> sluchs)
    {
        this.sluchs = sluchs;
    }

    public XElement Get(SankZ_mtr sank, string s_code)
    {
        XElement SANK = new XElement("SANK");

        string? SL_ID = sluchs?.FirstOrDefault()?.SL_ID?.Length < 20 ? string.Concat(sluchs?.FirstOrDefault()?.Id, "-", sluchs?.FirstOrDefault()?.SL_ID) : sluchs?.FirstOrDefault()?.SL_ID;

        //if (sank.S_CODE != null)
        SANK.Add(new XElement("S_CODE", string.Concat("MEK_", s_code, "-", sank.Id)));     
        if (SL_ID != null)
            SANK.Add(new XElement("SL_ID", SL_ID ));
        //if (sank.S_SUM != null)
            SANK.Add(new XElement("S_SUM", sank.S_SUM != null ? sank.S_SUM : 0));

        if (sank.S_TIP != null)
            SANK.Add(new XElement("S_TIP", sank.S_TIP));
        else
            SANK.Add(new XElement("S_TIP", 1));

        if (sank.S_OSN > 0)
            SANK.Add(new XElement("S_OSN", sank.S_OSN));
        //if (sank.DATE_ACT != null)
            SANK.Add(new XElement("DATE_ACT", sank.DATE_ACT != null ? sank.DATE_ACT?.ToString("yyyy-MM-dd"):""));
        //if (sank.NUM_ACT != null)
            SANK.Add(new XElement("NUM_ACT", sank.NUM_ACT != null && sank.NUM_ACT?.Length > 0 ? sank.NUM_ACT : "б/н"));            
        if (sank.S_COM != null)
            SANK.Add(new XElement("S_COM", sank?.S_COM.Length > 249 ? sank?.S_COM[..249] : sank?.S_COM));
        if (sank.S_IST != null)
            SANK.Add(new XElement("S_IST", sank.S_IST));        

        return SANK;
    }
}
