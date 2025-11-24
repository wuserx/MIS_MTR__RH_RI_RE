//XmlElement Сведения о законченном случае
public class Z_SL_RI_RE
{
    public XElement Get(ZakSluch_mtr zakSluch)
    {
        XElement Z_SL = new XElement("Z_SL");

        //Console.Write($"Z_SL_RE:{zakSluch.Id} ");

        IEnumerable<Sluch_mtr> sluchs = new RepositoryMTR(new MtrContext()).GetAllSluchByZakSluchIdAsync(zakSluch.Id).Result;
        
        IEnumerable<SankZ_mtr> sanks = new RepositoryMTR(new MtrContext()).GetAllSankZByZakIdAsync(zakSluch.Id).Result;
        
        IEnumerable<Expertise_mtr> ExpertiseSMOs = new RepositoryMTR(new MtrContext()).GetAllExpertiseSMOByZakIdAsync(sluchs).Result;

        IEnumerable<RepeatedMEK> RepeatedMEKs = new RepositoryMTR(new MtrContext()).GetRepeatedMEKByZakSluchsAsync(zakSluch.Id).Result;


        //если нет ни одного случая в трех таблицах SankZ, ExpertiseSMO, RepeatedMEK
        //то не формируем родительский NODE
        //для этого возвращаем пустой XElement
        if ((sanks.Count() + ExpertiseSMOs.Count() + RepeatedMEKs.Count()) <= 0)
            return Z_SL;

        

        if (zakSluch.IDCASEL != null || zakSluch.IDCASE != null)
            Z_SL.Add(new XElement("IDCASE", new RepositoryMTR(new MtrContext()).GetZakSluchIDCASE(zakSluch)) );
        if (zakSluch.SUMP != null)
            Z_SL.Add(new XElement("SUMP", zakSluch.SUMV == 0 ? zakSluch.SUMV : RepeatedMEKs.Count() > 0 ? (zakSluch.SUMV - RepeatedMEKs.Sum(s => s.S_SUM)) : zakSluch.SUMP));
        if (zakSluch.OPLATA != null)
            Z_SL.Add(new XElement("OPLATA", RepeatedMEKs.Count() > 0 ? RepeatedMEKs.FirstOrDefault()?.OPLATA : zakSluch.OPLATA));

        if (sanks.Count() > 0)
        {
            foreach (var sank in sanks)
            {
                Z_SL.Add(new SANK_RI_RE(sluchs).Get(sank, new RepositoryMTR(new MtrContext()).GetZakSluchIDCASE(zakSluch)) );
            }
        }

        if (ExpertiseSMOs.Count() > 0)
        {
            foreach (var ExpertiseSMO in ExpertiseSMOs)
            {
                Z_SL.Add(new ExpertiseSMO_RI_RE(sluchs).Get(ExpertiseSMO, new RepositoryMTR(new MtrContext()).GetZakSluchIDCASE(zakSluch)) );
            }
        }

        if (RepeatedMEKs.Count() > 0)
        {
            foreach (var repeatedMEK in RepeatedMEKs)
            {
                Z_SL.Add(new RepeatedMEK_RI_RE(sluchs).Get(repeatedMEK, new RepositoryMTR(new MtrContext()).GetZakSluchIDCASE(zakSluch)));
            }
        }
        
        Z_SL.Add(new XElement("SANK_IT", RepeatedMEKs.Count() > 0 ?  RepeatedMEKs.Sum(s => s.S_SUM) : zakSluch.SANK_IT));        

        return Z_SL;
    }
}