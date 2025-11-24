//XmlElement
public class ZL_LIST_RI
{
    // создаем Корневой элемент (Сведения о медпомощи)
    public XElement Get(Schet_mtr schet, IEnumerable<ZakSluch_mtr> ZakSluchs, string FileNameXml)
    {

        XElement ZL_LIST = new XElement("ZL_LIST");
        ZL_LIST.Add(new ZGLV_RI().Get(schet, FileNameXml));
        ZL_LIST.Add(new SCHET_RI().Get(schet));
        foreach (var ZakSluch in ZakSluchs)
        {
            ZL_LIST.Add(new ZAP_RI().Get(ZakSluch));
        }

        return ZL_LIST;
    }

    
}