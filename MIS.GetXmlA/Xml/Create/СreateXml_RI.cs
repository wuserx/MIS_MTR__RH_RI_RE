//Создание файлов H T D и C
public class СreateXml_RI
{  
    public void Run(Schet_mtr schet, IEnumerable<ZakSluch_mtr> ZakSluchs, string FileNameXml)
    {         
        XDocument XDOC = new XDocument();
        XDOC.Add(new ZL_LIST_RI().Get(schet, ZakSluchs, FileNameXml));
        XDOC.Save(new PathFileNameHelper().GetPathFfomsNoLFileXmlSave(FileNameXml, "MTR"));
    }
}
