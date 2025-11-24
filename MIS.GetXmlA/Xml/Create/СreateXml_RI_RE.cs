//Создание файлов H T D и C
public class СreateXml_RI_RE
{  
    public void Run(Schet_mtr schet, IEnumerable<ZakSluch_mtr> ZakSluchs, string FileNameXml)
    {
        IEnumerable<ZakSluch_mtr> ZakSluch_MTR_ExpertiseSMO = new RepositoryMTR(new MtrContext()).GetAllExpertiseSMOByZakSluchIdAsync(ZakSluchs).Result;
        IEnumerable<ZakSluch_mtr> ZakSluch_MTR_Sanks = new RepositoryMTR(new MtrContext()).GetAllSankZByZakSluchsAsync(ZakSluchs).Result;
        IEnumerable<ZakSluch_mtr> ZakSluch_RepeatedMEKs = new RepositoryMTR(new MtrContext()).GetAllRepeatedMEKByZakSluchIdAsync(ZakSluchs).Result;

        var ZakSluch_List = ZakSluch_RepeatedMEKs.Concat(ZakSluch_MTR_ExpertiseSMO.Concat(ZakSluch_MTR_Sanks));

        if (ZakSluch_List.Count() > 0)
        {           
            XDocument XDOC = new XDocument();

            //Версия взаимодействия 
            XDOC.Add(new ZGLV_RI_RE().Get(schet, ZakSluch_List, FileNameXml, out bool save_ok));

            if(save_ok)
                XDOC.Save(new PathFileNameHelper().GetPathFfomsNoLFileXmlSave(FileNameXml, "MTR"));
        }

        return;
    }
}
