//Создание файла L
public class СreateXmlL
{
    private Schet ctxSCHET;
    private string FileNameXml;
    private XDocument XDOC;

    public СreateXmlL()
    { }

    public СreateXmlL(Schet schet, string fileNameXml)
    {
        ctxSCHET = schet;
        FileNameXml = fileNameXml.ToUpper().Replace(".OMS", "");
        XDOC = new XDocument();
    }    

    public void Run()
    {
        XDOC.Add(new PERS_LIST(ctxSCHET, FileNameXml).Get());
        XDOC.Save(new PathFileNameHelper(ctxSCHET, FileNameXml).GetPathSmoLFileXmlSave());   
    }    
}
