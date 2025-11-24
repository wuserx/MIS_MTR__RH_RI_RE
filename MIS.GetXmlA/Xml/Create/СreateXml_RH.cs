//Создание файлов H T D и C
public class СreateXml_RH
{  
    public void Run(Schet schet, IEnumerable<ZakSluch> ZakSluchs, string FileNameXml)
    {         
        XDocument XDOC = new XDocument();
        XDOC.Add(new ZL_LIST_RH().Get(schet, ZakSluchs, FileNameXml));
        XDOC.Save(new PathFileNameHelper().GetPathFfomsNoLFileXmlSave(FileNameXml, "MIS"));
    }
}
