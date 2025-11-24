//XmlElement
public class PERS_ZGLV
{
    private Schet? ctxSCHET;
    private string? fileNameXml;

    public PERS_ZGLV()
    { }

    public PERS_ZGLV(Schet schet, string FileNameXml)
    {
        ctxSCHET = schet;
        fileNameXml = FileNameXml;
    }

    public XElement Get()
    {
        new RouteFileNameHerper(ctxSCHET, fileNameXml).GetAllName(out string PacketName, out string XmlName);        

        // создаем Заголовок файла
        XElement ZGLV = new XElement("ZGLV");
        //Версия взаимодействия 
        ZGLV.Add(new XElement("VERSION", "3.2"));// ctxSCHET.VERSION));
        // Дата 
        ZGLV.Add(new XElement("DATA", ctxSCHET.DATA?.ToString("yyyy-MM-dd")));
        // Имя файла
        ZGLV.Add(new XElement("FILENAME", new PathFileNameHelper().GetFilePrefixNameFromL(XmlName)));
        // Количество записей в файле
        ZGLV.Add(new XElement("FILENAME1", XmlName));
        return ZGLV;
    }
}