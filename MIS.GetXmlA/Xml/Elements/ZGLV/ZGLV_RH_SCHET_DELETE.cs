//XmlElement
public class ZGLV_RH_SCHET_DELETE
{
    public XElement Get(Schet schet, string FileNameXml)
    {
        string Version = "1.0";
            
        // создаем Заголовок файла
        XElement ZGLV = new XElement("ZGLV");
        //Версия взаимодействия 
        ZGLV.Add(new XElement("VERSION", Version));  // schet.VERSION);
        // Дата 
        ZGLV.Add(new XElement("DATA", schet.DATA?.ToString("yyyy-MM-dd")));
        // Имя файла
        ZGLV.Add(new XElement("FILENAME", FileNameXml));
        // Количество записей в файле
        ZGLV.Add(new XElement("SD_Z", schet.SD_Z));

        return ZGLV;
    }
}