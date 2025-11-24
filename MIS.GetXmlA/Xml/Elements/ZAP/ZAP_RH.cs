//XmlElement Записи
public class ZAP_RH
{    
    public XElement Get(ZakSluch zakSluch)
    {
        XElement ZAP = new XElement("ZAP");

        ZAP.Add(new XElement("N_ZAP", new RepositoryMIS(new MisContext()).GetZapN_ZAP(zakSluch)) );
        ZAP.Add(new PACIENT_RH().Get(zakSluch));
        ZAP.Add(new Z_SL_RH().Get(zakSluch));

        return ZAP;
    }
}