public class PERS_LIST
{
    private Schet ctxSCHET;
    private string fileNameXml;

    public PERS_LIST(Schet schet, string FileNameXml)
    {
        ctxSCHET = schet;
        fileNameXml = FileNameXml;
    }

    // создаем Корневой элемент (Сведения о медпомощи)
    public XElement Get()
    {
        List<Pers> perss = new RepositoryMIS(new MisContext()).GetGroupPersZakSluchBySchetId(ctxSCHET.Id, fileNameXml).Result;
        XElement PERS_LIST = new XElement("PERS_LIST");
        PERS_LIST.Add(new PERS_ZGLV(ctxSCHET, fileNameXml).Get());
        foreach (var pers in perss)
        {
            PERS_LIST.Add(new PERS(pers).Get());
        }

        return PERS_LIST;
    }
}
