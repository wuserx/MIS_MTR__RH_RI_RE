//Создание файлов H T D и C
public class СreateXml_RH_RE
{  
    public void Run(Schet schet, IEnumerable<ZakSluch> ZakSluchs, string FileNameXml)
    {
        IEnumerable<ZakSluch> ZakSluch_ExpertiseSMO = new RepositoryMIS(new MisContext()).GetAllExpertiseSMOByZakSluchsAsync(ZakSluchs).Result;
        IEnumerable<ZakSluch> ZakSluch_Sanks = new RepositoryMIS(new MisContext()).GetAllSankZByZakSluchsAsync(ZakSluchs).Result;
        IEnumerable<ZakSluch> ZakSluch_RepeatedMEKs = new RepositoryMIS(new MisContext()).GetAllRepeatedMEKByZakSluchsAsync(ZakSluchs).Result;

        
        var ZakSluch_List = ZakSluch_RepeatedMEKs.Concat(ZakSluch_ExpertiseSMO.Concat(ZakSluch_Sanks));

        //Console.WriteLine($"Счёт {schet.FILENAME} Всего RE {ZakSluch_List.Count()}шт.");

        if (ZakSluch_List.Count() > 0)
        {
            XDocument XDOC = new XDocument();

            //Версия взаимодействия
            XDOC.Add(new ZGLV_RH_RE().Get(schet, ZakSluch_List, FileNameXml, out bool save_ok));

            if (save_ok)
                XDOC.Save(new PathFileNameHelper().GetPathFfomsNoLFileXmlSave(FileNameXml, "MIS"));
        }

        return;
    }
}
