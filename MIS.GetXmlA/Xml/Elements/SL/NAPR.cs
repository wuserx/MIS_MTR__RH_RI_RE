public class NAPR
{
    private NApr Napr;

    public NAPR(NApr napr)
    {
        Napr = napr;
    }
    public XElement Get()
    {
        IEnumerable<Naz> nazs = new RepositoryMIS(new MisContext()).GetAllNazBySluchIdAsync(Napr.SluchId).Result;

        XElement NAPR = new XElement("NAPR");

        if (Napr.NAPR_DATE != null)
            NAPR.Add(new XElement("NAPR_DATE", Napr.NAPR_DATE?.ToString("yyyy-MM-dd")));
        if (Napr.NAPR_MO != null)
            NAPR.Add(new XElement("NAPR_MO", Napr.NAPR_MO));
        if (Napr.NAPR_V != null)
            NAPR.Add(new XElement("NAPR_V", Napr.NAPR_V));
        if (Napr.MET_ISSL != null)
            NAPR.Add(new XElement("MET_ISSL", Napr.MET_ISSL));
        if (Napr.NAPR_USL != null)
        {
            NAPR.Add(new XElement("NAPR_USL", Napr.NAPR_USL));
        }
        else
        {
            if (nazs.Count() > 0)
            {
                foreach (var naz in nazs)
                {
                    if (naz != null)
                        NAPR.Add(new XElement("NAPR_USL", naz.NAZ_USL));
                }
            }
                
            
        }

        if (nazs.Count() > 0)
        {
            foreach (var naz in nazs)
            {
                if (naz.NAZ_IDDOKT != null)
                    NAPR.Add(new XElement("NAZ_IDDOKT", naz.NAZ_IDDOKT));
            }
            foreach (var naz in nazs)
            {
                if (naz.NAZ_PMP != null)
                    NAPR.Add(new XElement("NAZ_PMP", naz.NAZ_PMP));
            }
            foreach (var naz in nazs)
            {
                if (naz.NAZ_PK != null)
                    NAPR.Add(new XElement("NAZ_PK", naz.NAZ_PK));
            }
        }


        
            
            

        return NAPR;
    }
}