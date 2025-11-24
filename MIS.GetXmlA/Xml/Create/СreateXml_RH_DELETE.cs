//Создание файлов H T D и C
public class СreateXml_RH_DELETE
{  
    public void Run(Schet schet, IEnumerable<ZakSluch> ZakSluchs, string FileNameXml)
    {         
        XDocument XDOC = new XDocument();
        XDOC.Add(new ZL_LIST_RH_SCHET_DELETE().Get(schet, ZakSluchs, FileNameXml));
        XDOC.Save(new PathFileNameHelper().GetPathFfomsNoLSchetDeleteFileXmlSave(FileNameXml));
    }
}
