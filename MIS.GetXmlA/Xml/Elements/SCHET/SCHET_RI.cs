//XmlElement
public class SCHET_RI
{ 
    public XElement Get(Schet_mtr schet)
    {
        // создаем Счёт
        XElement SCHET = new XElement("SCHET");

        if (schet.CODE != null)
            SCHET.Add(new XElement("CODE", new RepositoryMTR(new MtrContext()).GetSchetCODE(schet)));
        if (schet.YEAR != null)
            SCHET.Add(new XElement("YEAR", schet.YEAR));
        if (schet.MONTH != null)
            SCHET.Add(new XElement("MONTH", schet.MONTH));
        if (schet.NSCHET != null)
            SCHET.Add(new XElement("NSCHET", schet.NSCHET));
        if (schet.DSCHET != null)
            SCHET.Add(new XElement("DSCHET", schet.DSCHET?.ToString("yyyy-MM-dd")));
        if (schet.COMENTS != null)
            SCHET.Add(new XElement("COMENTS", schet.COMENTS));       

        return SCHET;
    }
}