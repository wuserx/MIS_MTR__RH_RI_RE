public class NAZ
{
    private Naz Naz;

    public NAZ(Naz naz)
    {
        Naz = naz;
    }

    public XElement Get()
    {
        XElement NAZ = new XElement("NAZ");
        if (Naz.NAZ_N != null)
            NAZ.Add(new XElement("NAZ_N", Naz.NAZ_N));
        if (Naz.NAZ_R != null)
            NAZ.Add(new XElement("NAZ_R", Naz.NAZ_R));
        if (Naz.NAZ_SP != null)
            NAZ.Add(new XElement("NAZ_SP", Naz.NAZ_SP));
        if (Naz.NAZ_V != null)
            NAZ.Add(new XElement("NAZ_V", Naz.NAZ_V));
        if (Naz.NAZ_USL != null)
            NAZ.Add(new XElement("NAZ_USL", Naz.NAZ_USL));
        if (Naz.NAPR_DATE != null)
            NAZ.Add(new XElement("NAPR_DATE", Naz.NAPR_DATE?.ToString("yyyy-MM-dd")));
        if (Naz.NAPR_MO != null)
            NAZ.Add(new XElement("NAPR_MO", Naz.NAPR_MO));
        if (Naz.NAZ_PMP != null)
            NAZ.Add(new XElement("NAZ_PMP", Naz.NAZ_PMP));
        if (Naz.NAZ_PK != null)
            NAZ.Add(new XElement("NAZ_PK", Naz.NAZ_PK));

        return NAZ;
    }
}
