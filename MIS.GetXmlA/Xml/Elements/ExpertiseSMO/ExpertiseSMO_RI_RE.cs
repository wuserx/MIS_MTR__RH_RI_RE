public class ExpertiseSMO_RI_RE
{
    private readonly IEnumerable<Sluch_mtr> sluchs;

    public ExpertiseSMO_RI_RE(IEnumerable<Sluch_mtr> sluchs)
    {
        this.sluchs = sluchs;
    }
    public XElement Get(Expertise_mtr expertiseSMO, string s_code)
    {
        XElement SANK = new XElement("SANK");

        string SL_ID = sluchs.FirstOrDefault()?.SL_ID?.Length < 20 ? string.Concat(sluchs.FirstOrDefault()?.Id, "-", sluchs.FirstOrDefault()?.SL_ID) : sluchs.FirstOrDefault()?.SL_ID;

        SANK.Add(new XElement("S_CODE", string.Concat("MEE-EKMP_", expertiseSMO.Id, "-", s_code, "-", expertiseSMO.Id)));
        if(SL_ID != null)
            SANK.Add(new XElement("SL_ID", SL_ID));
        SANK.Add(new XElement("S_SUM", expertiseSMO.SumExpertise));        
        SANK.Add(new XElement("S_TIP", expertiseSMO?.IDVID));
        if (expertiseSMO.KodOtk > 0)
            SANK.Add(new XElement("S_OSN", expertiseSMO.KodOtk));        
        SANK.Add(new XElement("DATE_ACT", expertiseSMO.DAktExpertiseMee == null ? expertiseSMO.DAktExpertiseEkmp?.ToString("yyyy-MM-dd") : expertiseSMO.DAktExpertiseMee?.ToString("yyyy-MM-dd")));        
        SANK.Add(new XElement("NUM_ACT", expertiseSMO.NAktExpertiseMEE != null && expertiseSMO.NAktExpertiseMEE?.Length > 0 ? expertiseSMO.NAktExpertiseEKMP : "б/н"));        
        SANK.Add(new XElement("CODE_EXP", expertiseSMO.CodeExps?.Replace("-","").Replace(" ", "")));
        if (expertiseSMO.Comment != null)
            SANK.Add(new XElement("S_COM", expertiseSMO.Comment.Length >= 249 ? expertiseSMO.Comment[..249] : expertiseSMO.Comment));       
        SANK.Add(new XElement("S_IST", 1));
        if (expertiseSMO.SumExpertisePenalty >= 0)
            SANK.Add(new XElement("STRAF", expertiseSMO.SumExpertisePenalty));

        return SANK;
    }
}
