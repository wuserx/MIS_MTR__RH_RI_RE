//XmlElement
public class PROTOCOL
{
    private Schet ctxSCHET;

    public PROTOCOL(Schet schet)
    {
        ctxSCHET = schet;
    }

    public XElement Get()
    {
        // создаем Счёт
        XElement SCHET = new XElement("PROTOCOL");
        if (ctxSCHET.CODE != null)
            SCHET.Add(new XElement("CODE", ctxSCHET.CODE));

        if (ctxSCHET.YEAR != null)
            SCHET.Add(new XElement("YEAR", ctxSCHET.YEAR));
        if (ctxSCHET.MONTH != null)
            SCHET.Add(new XElement("MONTH", ctxSCHET.MONTH));

        if (ctxSCHET.NSCHET != null)
            SCHET.Add(new XElement("NSCHET", ctxSCHET.NSCHET));        

        if (ctxSCHET?.SUMMAV != null)
            SCHET.Add(new XElement("SUMMAV", ctxSCHET.SUMMAV)); 

        if (Char.ToUpper(ctxSCHET.FILENAME[0]) == 'H')
        {
            if (ctxSCHET?.SUMMAV_PDF != null && ctxSCHET?.SUMMAV_PDF > 0)
                SCHET.Add(new XElement("SUMMAV_PDF", ctxSCHET.SUMMAV_PDF)); 
            if (ctxSCHET?.SUMMAV_SMP != null && ctxSCHET?.SUMMAV_SMP > 0)
                SCHET.Add(new XElement("SUMMAV_SMP", ctxSCHET.SUMMAV_SMP));
            if (ctxSCHET?.SUMMAV_FAP != null && ctxSCHET?.SUMMAV_FAP > 0)
                SCHET.Add(new XElement("SUMMAV_FAP", ctxSCHET.SUMMAV_FAP));


            if (ctxSCHET?.SUMMAP_PDF != null && ctxSCHET?.SUMMAP_PDF > 0)
                SCHET.Add(new XElement("SUMMAP_PDF", ctxSCHET.SUMMAP_PDF)); 
            if (ctxSCHET?.SUMMAP_SMP != null && ctxSCHET?.SUMMAP_SMP > 0)
                SCHET.Add(new XElement("SUMMAP_SMP", ctxSCHET.SUMMAP_SMP));
            if (ctxSCHET?.SUMMAP_FAP != null && ctxSCHET?.SUMMAP_FAP > 0)
                SCHET.Add(new XElement("SUMMAP_FAP", ctxSCHET.SUMMAP_FAP));
        }        

        if (ctxSCHET?.SUMMAP != null)
            SCHET.Add(new XElement("SUMMAP", ctxSCHET.SUMMAP)); 
       
        

        if (ctxSCHET.DSCHET != null)
            SCHET.Add(new XElement("DSCHET", ctxSCHET.DSCHET?.ToString("yyyy-MM-dd")));
        if (ctxSCHET.PLAT != null)
            SCHET.Add(new XElement("SMO", ctxSCHET.PLAT));
        if (ctxSCHET.CODE_MO != null)
            SCHET.Add(new XElement("CODE_MO", ctxSCHET.CODE_MO));

        if (ctxSCHET?.SANK_MEK != null)
            SCHET.Add(new XElement("SANK_MEK", ctxSCHET.SANK_MEK));      

        return SCHET;
    }
}