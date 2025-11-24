//XmlElement Записи
public class ZAP_RI
{    
    public XElement Get(ZakSluch_mtr zakSluch)
    {
        XElement ZAP = new XElement("ZAP");

        ZAP.Add(new XElement("N_ZAP", new RepositoryMTR(new MtrContext()).GetZapN_ZAP(zakSluch)));
        ZAP.Add(new PACIENT_RI().Get(zakSluch));
        ZAP.Add(new Z_SL_RI().Get(zakSluch));

        return ZAP;
    }
}