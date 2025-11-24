public class KSG_KPG_RH
{
    private KsgKpg Ksg;

    public KSG_KPG_RH(KsgKpg ksg)
    {
        Ksg = ksg;
    }

    public XElement Get()
    {
        XElement KSG = new XElement("KSG_KPG");

        if (Ksg.N_KSG != null)
            KSG.Add(new XElement("N_KSG", Ksg.N_KSG));
        if (Ksg.VER_KSG != null)
            KSG.Add(new XElement("VER_KSG", Ksg.VER_KSG));
        if (Ksg.KSG_PG != null)
            KSG.Add(new XElement("KSG_PG", Ksg.KSG_PG));
        if (Ksg.N_KPG != null)
            KSG.Add(new XElement("N_KPG", Ksg.N_KPG));
        if (Ksg.KOEF_Z != null)
            KSG.Add(new XElement("KOEF_Z", Ksg.KOEF_Z));
        if (Ksg.KOEF_UP != null)
            KSG.Add(new XElement("KOEF_UP", Ksg.KOEF_UP));
        if (Ksg.BZTSZ != null)
            KSG.Add(new XElement("BZTSZ", Ksg.BZTSZ));
        if (Ksg.KOEF_D != null)
            KSG.Add(new XElement("KOEF_D", Ksg.KOEF_D));
        if (Ksg.KOEF_U != null)
            KSG.Add(new XElement("KOEF_U", Ksg.KOEF_U));
        if (Ksg.Crits != null)
        {
            foreach (var Crit in Ksg.Crits)
            {
                KSG.Add(new XElement("CRIT", Crit));
            }
        }
        if (Ksg.SL_K != null)
            KSG.Add(new XElement("SL_K", Ksg.SL_K));

        /*элемент должен отсутствовать при выполнении условия - отсутствует SL_KOEF*/
        if (Ksg.IT_SL != null && Ksg._SluchKoefs != null)
            KSG.Add(new XElement("IT_SL", Ksg.IT_SL));

        if (Ksg.SluchKoefs != null && Ksg._SluchKoefs != null)
        {
            foreach (var slkoefs in Ksg._SluchKoefs)
            {
                XElement SLKOEF = new XElement("SL_KOEF");

                if (slkoefs.IDSL != null)
                    SLKOEF.Add(new XElement("IDSL", slkoefs.IDSL));
                if (slkoefs.Z_SL != null)
                    SLKOEF.Add(new XElement("Z_SL", slkoefs.Z_SL));
                KSG.Add(SLKOEF);
            }
        }           
            
        return KSG;
    }
}