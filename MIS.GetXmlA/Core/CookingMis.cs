public class CookingMis
{
    public static void Run(Schet schet, string FileNameXml)
    {
        //выбираем з.случаи для перебираемого пакета
        IEnumerable<ZakSluch> ZakSluchs = new RepositoryMIS(new MisContext()).GetAllZakSluchBySchetIdAsync(schet.Id).Result;

        //фильтр - если результат не пустой
        if (ZakSluchs.Count() <= 0)
            return;
        
        if (Init.TYPE_OUT_XML_RH == 1)
            new СreateXml_RH().Run(schet, ZakSluchs, FileNameXml.Replace("D", "H"));

        if (Init.GET_EMPTY_FROM_MISDB == 1)
            new СreateXml_RH_DELETE().Run(schet, ZakSluchs, FileNameXml.Replace("D", "H"));

        if (Init.TYPE_OUT_XML_RHE == 1)
            new СreateXml_RH_RE().Run(schet, ZakSluchs, FileNameXml.Replace("D", "E"));

        if (Init.GET_FROM_MISDB_IDENT == 1)
            new СreateCsvIdentFromMisDB().Run(schet, ZakSluchs, FileNameXml.Replace("D", ""));

    }
}