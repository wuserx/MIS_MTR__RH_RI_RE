//XmlElement
public class ZGLV_RI
{
    public XElement Get(Schet_mtr schet, string FileNameXml)
    {
        string Version = Init.YEAR_REPORT <= 2024 ? "R1.1" : "R1.2";

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