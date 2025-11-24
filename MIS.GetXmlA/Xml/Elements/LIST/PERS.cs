public class PERS
{
    private Pers Zaksluch;

    public PERS(Pers zaksluch)
    {
        Zaksluch = zaksluch;
    }

    public XElement Get()
    {
        XElement PERS = new XElement("PERS");
        if (Zaksluch.ID_PAC != null)
            PERS.Add(new XElement("ID_PAC", Zaksluch.ID_PAC));
        if (Zaksluch.FAM != null)
            PERS.Add(new XElement("FAM", Zaksluch.FAM));
        if (Zaksluch.IM != null)
            PERS.Add(new XElement("IM", Zaksluch.IM));
        if (Zaksluch.OT != null)
            PERS.Add(new XElement("OT", Zaksluch.OT));
        if (Zaksluch.W != null)
            PERS.Add(new XElement("W", Zaksluch.W));
        if (Zaksluch.DR != null)
            PERS.Add(new XElement("DR", Zaksluch.DR?.ToString("yyyy-MM-dd")));
        if (Zaksluch.Dosts != null)
        {
            foreach (var dost in Zaksluch.Dosts)
            {
                PERS.Add(new XElement("DOST", dost));
            }            
        }
        if (Zaksluch.TEL != null)
            PERS.Add(new XElement("TEL", Zaksluch.TEL));
        if (Zaksluch.FAM_P != null)        
            PERS.Add(new XElement("FAM_P", Zaksluch.FAM_P));
        if (Zaksluch.IM_P != null)
            PERS.Add(new XElement("IM_P", Zaksluch.IM_P));
        if (Zaksluch.OT_P != null)
            PERS.Add(new XElement("OT_P", Zaksluch.OT_P));
        if (Zaksluch.W_P != null)
            PERS.Add(new XElement("W_P", Zaksluch.W_P));
        if (Zaksluch.DR_P != null)
            PERS.Add(new XElement("DR_P", Zaksluch.DR_P?.ToString("yyyy-MM-dd")));
        if (Zaksluch.DostPs != null)
        {
            foreach (var dostp in Zaksluch.DostPs)
            {
                PERS.Add(new XElement("DOST_P", dostp));
            }            
        }
        
        if (Zaksluch.MR != null)
            PERS.Add(new XElement("MR", Zaksluch.MR));
        if (Zaksluch.DOCTYPE != null)
            PERS.Add(new XElement("DOCTYPE", Zaksluch.DOCTYPE));
        if (Zaksluch.DOCSER != null)
            PERS.Add(new XElement("DOCSER", Zaksluch.DOCSER));
        if (Zaksluch.DOCNUM != null)
            PERS.Add(new XElement("DOCNUM", Zaksluch.DOCNUM));
        if (Zaksluch.DOCDATE != null)
            PERS.Add(new XElement("DOCDATE", Zaksluch.DOCDATE?.ToString("yyyy-MM-dd")));
        if (Zaksluch.DOCORG != null)
            PERS.Add(new XElement("DOCORG", Zaksluch.DOCORG));
        if (Zaksluch.SNILS != null)
            PERS.Add(new XElement("SNILS", Zaksluch.SNILS));
        if (Zaksluch.OKATOG != null)
            PERS.Add(new XElement("OKATOG", Zaksluch.OKATOG));
        if (Zaksluch.OKATOP != null)
            PERS.Add(new XElement("OKATOP", Zaksluch.OKATOP));
        //PERS.Add(new XElement("COMENTP", Zaksluch.COMENTP));

        return PERS;
    }
}
