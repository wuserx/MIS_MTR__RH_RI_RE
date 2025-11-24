public class ExpertiseSMO_RH_RE
{
    private readonly IEnumerable<Sluch> sluchs;

    public ExpertiseSMO_RH_RE(IEnumerable<Sluch> sluchs)
    {
        this.sluchs = sluchs;
    }
    public XElement Get(ExpertiseSMO expertiseSMO, string s_code)
    {
        XElement SANK = new XElement("SANK");

        string? SL_ID = sluchs?.FirstOrDefault()?.SL_ID?.Length < 20 ? string.Concat(sluchs?.FirstOrDefault()?.Id, "-", sluchs?.FirstOrDefault()?.SL_ID) : sluchs?.FirstOrDefault()?.SL_ID;

        SANK.Add(new XElement("S_CODE", string.Concat("MEE-EKMP_", s_code, "-", expertiseSMO.Id)) ); 
        if (SL_ID != null)
            SANK.Add(new XElement("SL_ID", SL_ID));        
        SANK.Add(new XElement("S_SUM", expertiseSMO.SANK_EKMP + expertiseSMO.SANK_MEE));        
        SANK.Add(new XElement("S_TIP", expertiseSMO?.TYPE_EXPERT));
        if (expertiseSMO?.REFREASON?.Length > 0)
            SANK.Add(new XElement("S_OSN", expertiseSMO.REFREASON));        
        SANK.Add(new XElement("DATE_ACT", expertiseSMO?.DSCHET.ToString("yyyy-MM-dd")));        
        SANK.Add(new XElement("NUM_ACT", expertiseSMO?.NSCHET != null && expertiseSMO.NSCHET?.Length > 0 ? expertiseSMO.NSCHET : "б/н"));        
        SANK.Add(new XElement("CODE_EXP", expertiseSMO?.SNILS?.Replace("-","").Replace(" ", "")));
        if (expertiseSMO.ZAKL != null)
            SANK.Add(new XElement("S_COM", expertiseSMO.ZAKL.Length >= 249 ? expertiseSMO.ZAKL[..249] : expertiseSMO.ZAKL));       
        SANK.Add(new XElement("S_IST", 1));
        if (expertiseSMO.SHTRAF >= 0)
            SANK.Add(new XElement("STRAF", expertiseSMO.SHTRAF));

        return SANK;
    }
}
