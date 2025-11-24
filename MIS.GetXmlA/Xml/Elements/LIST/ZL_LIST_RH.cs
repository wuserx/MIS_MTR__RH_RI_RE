//XmlElement
public class ZL_LIST_RH
{
    // создаем Корневой элемент (Сведения о медпомощи)
    public XElement Get(Schet schet, IEnumerable<ZakSluch> ZakSluchs, string FileNameXml)
    {

        XElement ZL_LIST = new XElement("ZL_LIST");
        ZL_LIST.Add(new ZGLV_RH().Get(schet, FileNameXml));
        ZL_LIST.Add(new SCHET_RH().Get(schet));
        foreach (var ZakSluch in ZakSluchs)
        {
            ZL_LIST.Add(new ZAP_RH().Get(ZakSluch));
        }

        return ZL_LIST;
    }

    
}