//XmlElement
public class ZGLV_RH_RE
{
    public XElement Get(Schet schet, IEnumerable<ZakSluch> ZakSluchs, string FileNameXml, out bool save_ok)
    {
        string Version = "E1.1";

        // создаем Заголовок файла
        XElement EXP_LIST = new XElement("EXP_LIST");

        XElement ZGLV = new XElement("ZGLV");
        //Версия взаимодействия 
        ZGLV.Add(new XElement("VERSION", Version));  // schet.VERSION);
        // Дата 
        ZGLV.Add(new XElement("DATA", schet.DATA?.ToString("yyyy-MM-dd")));
        // Имя файла
        ZGLV.Add(new XElement("FILENAME", FileNameXml));


        XElement EXP = new XElement("EXP");

        int count_zap = 0;
        foreach (var ZakSluch in ZakSluchs)
        {
            var EXP_ZAP_RH = new EXP_ZAP_RH().Get(schet, ZakSluch);

            //Console.WriteLine($"ZakSluch.Id {ZakSluch.Id} EXP_ZAP_RH.Elements cnt: {EXP_ZAP_RH.Elements().Count()}");

            if (EXP_ZAP_RH.Elements().Count() > 0)
            {
                //Console.WriteLine(".");

                EXP.Add(EXP_ZAP_RH);

                count_zap+=1;
            }
        }

        // Количество записей в файле
        ZGLV.Add(new XElement("SD_Z", count_zap));
        

        EXP_LIST.Add(ZGLV);

        save_ok = count_zap > 0 ? true:false;

        EXP_LIST.Add(EXP);

        return EXP_LIST;
    }
}