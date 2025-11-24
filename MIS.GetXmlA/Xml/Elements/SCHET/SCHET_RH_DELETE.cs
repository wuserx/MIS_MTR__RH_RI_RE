//XmlElement
public class SCHET_RH_DELETE
{ 
    public XElement Get(Schet schet)
    {
        // создаем Счёт
        XElement SCHET = new XElement("SCHET");

        if (schet.CODE != null)
            SCHET.Add(new XElement("CODE", new RepositoryMIS(new MisContext()).GetSchetCODE(schet)));
        if (schet.YEAR != null)
            SCHET.Add(new XElement("YEAR", schet.YEAR_REPORT));
        if (schet.MONTH != null)
            SCHET.Add(new XElement("MONTH", schet.MONTH_REPORT));
        if (schet.NSCHET != null)
            SCHET.Add(new XElement("NSCHET", schet.NSCHET));
        if (schet.DSCHET != null)
            SCHET.Add(new XElement("DSCHET", schet.DSCHET?.ToString("yyyy-MM-dd")));
        if (schet.COMENTS != null)
            SCHET.Add(new XElement("COMENTS", schet.COMENTS));       

        return SCHET;
    }
}