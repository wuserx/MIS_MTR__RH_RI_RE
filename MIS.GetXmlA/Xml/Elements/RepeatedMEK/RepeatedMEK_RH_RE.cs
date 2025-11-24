public class RepeatedMEK_RH_RE
{
    private readonly IEnumerable<Sluch> sluchs;

    public RepeatedMEK_RH_RE(IEnumerable<Sluch> sluchs)
    {
        this.sluchs = sluchs;
    }

    public XElement Get(RepeatedMEK repeatedMek, string s_code)
    {
        XElement SANK = new XElement("SANK");

        string? SL_ID = sluchs?.FirstOrDefault()?.SL_ID?.Length < 20 ? string.Concat(sluchs?.FirstOrDefault()?.Id, "-", sluchs?.FirstOrDefault()?.SL_ID) : sluchs?.FirstOrDefault()?.SL_ID;

        //if (sank.S_CODE != null)
        SANK.Add(new XElement("S_CODE", string.Concat("RMEK_", s_code, "-", repeatedMek.Id)) );     
        if (SL_ID != null)
            SANK.Add(new XElement("SL_ID", SL_ID ));
        //if (sank.S_SUM != null)
        SANK.Add(new XElement("S_SUM", repeatedMek?.S_SUM != null ? repeatedMek.S_SUM : 0));

        if (repeatedMek?.S_TIP != null)
            SANK.Add(new XElement("S_TIP", repeatedMek.S_TIP));
        else
            SANK.Add(new XElement("S_TIP", 12));

        if (repeatedMek?.S_OSN > 0)
            SANK.Add(new XElement("S_OSN", repeatedMek.S_OSN));
        //if (sank.DATE_ACT != null)
            SANK.Add(new XElement("DATE_ACT", repeatedMek?.DATE_ACT != null ? repeatedMek?.DATE_ACT?.ToString("yyyy-MM-dd"):""));
        //if (sank.NUM_ACT != null)
            SANK.Add(new XElement("NUM_ACT", repeatedMek?.NUM_ACT != null && repeatedMek.NUM_ACT?.Length > 0 ? repeatedMek.NUM_ACT : "б/н"));            
        if (repeatedMek?.S_COM != null)
            SANK.Add(new XElement("S_COM", repeatedMek?.S_COM.Length > 249 ? repeatedMek?.S_COM[..249] : repeatedMek?.S_COM));
        if (repeatedMek?.S_IST > -1)
            SANK.Add(new XElement("S_IST", repeatedMek.S_IST));        

        return SANK;
    }
}
