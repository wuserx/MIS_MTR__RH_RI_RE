//XmlElement Сведения о законченном случае
public class Z_SL_RH_RE
{
    public XElement Get(ZakSluch zakSluch)
    {
        XElement Z_SL = new XElement("Z_SL");

        
        //Console.WriteLine($"Запрос SankZ {DateTime.Now.ToString("yy.MM.dd - HH:mm:ss")}");
        IEnumerable<SankZ> sanks = new RepositoryMIS(new MisContext()).GetAllSankZByZakIdAsync(zakSluch.Id).Result;
        //Console.WriteLine($"Запрос ExpertiseSMO {DateTime.Now.ToString("yy.MM.dd - HH:mm:ss")}");
        IEnumerable<ExpertiseSMO> ExpertiseSMOs = new RepositoryMIS(new MisContext()).GetAllExpertiseSMOByZakIdAsync(zakSluch.Id).Result;
        //Console.WriteLine($"Запрос RepeatedMEK {DateTime.Now.ToString("yy.MM.dd - HH:mm:ss")}");
        IEnumerable<RepeatedMEK> RepeatedMEKs = new RepositoryMIS(new MisContext()).GetRepeatedMEKByZakSluchsAsync(zakSluch.Id).Result;
        
        //Console.WriteLine($"Обработка S {sanks.Count()}шт. E {ExpertiseSMOs.Count()}шт. R {RepeatedMEKs.Count()}шт. {DateTime.Now.ToString("yy.MM.dd - HH:mm:ss")}");


        //если нет ни одного случая в трех таблицах SankZ, ExpertiseSMO, RepeatedMEK
        //то не формируем родительский NODE
        //для этого возвращаем пустой XElement
        if ((sanks.Count() + ExpertiseSMOs.Count() + RepeatedMEKs.Count()) <= 0)
            return Z_SL;

        //Console.WriteLine($"Запрос случаев {DateTime.Now.ToString("yy.MM.dd - HH:mm:ss")}");
        IEnumerable<Sluch> sluchs = new RepositoryMIS(new MisContext()).GetAllSluchByZakSluchIdAsync(zakSluch.Id).Result;


        if (zakSluch.IDCASEL != null || zakSluch.IDCASE != null)
            Z_SL.Add(new XElement("IDCASE", new RepositoryMIS(new MisContext()).GetZakSluchIDCASE(zakSluch)) );
        if (zakSluch.SUMP != null)
            Z_SL.Add(new XElement("SUMP", zakSluch.SUMV == 0 ? zakSluch.SUMV : RepeatedMEKs.Count() > 0 ? (zakSluch.SUMV - RepeatedMEKs.Sum(s => s.S_SUM)) : zakSluch.SUMP));
        if (zakSluch.OPLATA > 0)
            Z_SL.Add(new XElement("OPLATA", RepeatedMEKs.Count() > 0 ? RepeatedMEKs.FirstOrDefault()?.OPLATA : zakSluch.OPLATA));

        if (sanks.Count() > 0)
        {
            foreach (var sank in sanks)
            {
                Z_SL.Add(new SANK_RH_RE(sluchs).Get(sank, new RepositoryMIS(new MisContext()).GetZakSluchIDCASE(zakSluch)) );
            }
        }

        if (ExpertiseSMOs.Count() > 0)
        {
            foreach (var ExpertiseSMO in ExpertiseSMOs)
            {
                Z_SL.Add(new ExpertiseSMO_RH_RE(sluchs).Get(ExpertiseSMO, new RepositoryMIS(new MisContext()).GetZakSluchIDCASE(zakSluch)) );
            }
        }

        if (RepeatedMEKs.Count() > 0)
        {
            foreach (var repeatedMEK in RepeatedMEKs)
            {
                Z_SL.Add(new RepeatedMEK_RH_RE(sluchs).Get(repeatedMEK, new RepositoryMIS(new MisContext()).GetZakSluchIDCASE(zakSluch)));
            }
        }
        
        Z_SL.Add(new XElement("SANK_IT", RepeatedMEKs.Count() > 0 ? RepeatedMEKs.Sum(s => s.S_SUM) : zakSluch.SANK_IT));        

        return Z_SL;
    }
}