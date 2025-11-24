//XmlElement
public class ZL_LIST_RH_SCHET_DELETE
{
    // создаем Корневой элемент (Сведения о медпомощи)
    public XElement Get(Schet schet, IEnumerable<ZakSluch> ZakSluchs, string FileNameXml)
    {

        XElement ZL_LIST = new XElement("ZL_LIST");
        ZL_LIST.Add(new ZGLV_RH_SCHET_DELETE().Get(schet, FileNameXml));
        ZL_LIST.Add(new SCHET_RH_DELETE().Get(schet));

        return ZL_LIST;
    }

    
}