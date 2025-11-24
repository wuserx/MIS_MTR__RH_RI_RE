public class SL_KOEF
{
    private SluchKoef sluchKoef;

    public SL_KOEF(SluchKoef sluchkoef)
    {
        sluchKoef = sluchkoef;
    }

    public XElement Get()
    {
        XElement SLKOEF = new XElement("SL_KOEF");
        if (sluchKoef.IDSL != null)
            SLKOEF.Add(new XElement("IDSL", sluchKoef.IDSL));
        if (sluchKoef.Z_SL != null)
            SLKOEF.Add(new XElement("Z_SL", sluchKoef.Z_SL));

        return SLKOEF;
    }
}