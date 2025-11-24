public class CookingMtr
{   
    public static void Run(Schet_mtr schet, string FileNameXml)
    {
        //выбираем з.случаи для перебираемого пакета
        IEnumerable<ZakSluch_mtr> ZakSluchs = new RepositoryMTR(new MtrContext()).GetAllZakSluchBySchetIdAsync(schet.Id).Result;

        //фильтр - если результат не пустой
        if (ZakSluchs.Count() <= 0)
            return;

        
        if (Init.TYPE_OUT_XML_RI == 1)
            new СreateXml_RI().Run(schet, ZakSluchs, FileNameXml.Replace("D", "I"));

        if(Init.GET_EMPTY_FROM_MTRDB == 1)
            new СreateXml_RI_DELETE().Run(schet, ZakSluchs, FileNameXml.Replace("D", "I"));

        if (Init.TYPE_OUT_XML_RIE == 1)
            new СreateXml_RI_RE().Run(schet, ZakSluchs, FileNameXml.Replace("D", "E"));

        if (Init.GET_FROM_MTRDB_IDENT == 1)
            new СreateCsvIdentFromMtrDB().Run(schet, ZakSluchs, FileNameXml.Replace("D", ""));
    }  
}