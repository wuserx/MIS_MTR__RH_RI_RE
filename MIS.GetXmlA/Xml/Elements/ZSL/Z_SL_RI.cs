//XmlElement Сведения о законченном случае
public class Z_SL_RI
{
    public XElement Get(ZakSluch_mtr zakSluch)
    {
        //Console.Write($"Z_SL:{zakSluch.Id} ");

        IEnumerable<Sluch_mtr> sluchs = new RepositoryMTR(new MtrContext()).GetAllSluchByZakSluchIdAsync(zakSluch.Id).Result;
        IEnumerable<RepeatedMEK> RepeatedMEKs = new RepositoryMTR(new MtrContext()).GetRepeatedMEKByZakSluchsAsync(zakSluch.Id).Result;

        var s_sum_RepeatedMEKs = RepeatedMEKs.Sum(s => s.S_SUM);
        var oplata_RepeatedMEKs = RepeatedMEKs.FirstOrDefault();

        XElement Z_SL = new XElement("Z_SL");

        if (zakSluch.IDCASEL != null || zakSluch.IDCASE != null)
            Z_SL.Add(new XElement("IDCASE", string.Concat(zakSluch.Id,"-", zakSluch.IDCASEL != null ? zakSluch.IDCASEL : zakSluch.IDCASE)) );
        if (zakSluch.USL_OK != null)
            Z_SL.Add(new XElement("USL_OK", zakSluch.USL_OK));
        if (zakSluch.VIDPOM != null)
            Z_SL.Add(new XElement("VIDPOM", zakSluch.VIDPOM));
        //if (zakSluch.FOR_POM != null)
            Z_SL.Add(new XElement("FOR_POM", zakSluch.FOR_POM != null ? zakSluch.FOR_POM : 3));
        if (zakSluch.NPR_MO != null)
            Z_SL.Add(new XElement("NPR_MO", zakSluch.NPR_MO));
        if (zakSluch.NPR_DATE != null)
            Z_SL.Add(new XElement("NPR_DATE", zakSluch.NPR_DATE?.ToString("yyyy-MM-dd")));
        if (zakSluch.DISP != null)
            Z_SL.Add(new XElement("DISP_TYPE", zakSluch.DISP));
        if (zakSluch.FILENAME != null)
            if (new List<string> { "DV", "DB" }.Contains(zakSluch.FILENAME[..2]))
                Z_SL.Add(new XElement("P_DISP2", 1));
        
        if (zakSluch.LPU != null)
            Z_SL.Add(new XElement("LPU", zakSluch.LPU));
        if (zakSluch.DATE_Z_1 != null)
            Z_SL.Add(new XElement("DATE_Z_1", zakSluch.DATE_Z_1?.ToString("yyyy-MM-dd")));
        if (zakSluch.DATE_Z_2 != null)
            Z_SL.Add(new XElement("DATE_Z_2", zakSluch.DATE_Z_2?.ToString("yyyy-MM-dd")));
        if (zakSluch.KD_Z != null)
            Z_SL.Add(new XElement("KD_Z", zakSluch.KD_Z));
        if (zakSluch.VNOV_Ms != null)
            Z_SL.Add(new XElement("VNOV_M", zakSluch.VNOV_Ms));
        if (zakSluch.RSLT != null)
        {
            Z_SL.Add(new XElement("RSLT", zakSluch.RSLT));
        }
        else
        {
            if (zakSluch.RSLT_D != null)
                Z_SL.Add(new XElement("RSLT", zakSluch.RSLT_D));
        }
        
        Z_SL.Add(new XElement("ISHOD", zakSluch.ISHOD != null ? zakSluch.ISHOD : 306));
        if (zakSluch._OsSluchs != null)
        {
            foreach (var OsSluch in zakSluch._OsSluchs)
            {
                Z_SL.Add(new XElement("OS_SLUCH", OsSluch));
            }
        }
        if (zakSluch.VB_P != null)
            Z_SL.Add(new XElement("VB_P", zakSluch.VB_P));
        foreach (var sluch in sluchs)
        {            
            Z_SL.Add(new SL_RI_Xml(sluch).Get(zakSluch));            
        }
        if (zakSluch.IDSP != null)
            Z_SL.Add(new XElement("IDSP", zakSluch.IDSP));
        if (zakSluch.SUMV != null)
            Z_SL.Add(new XElement("SUMV", zakSluch.SUMV));
        Z_SL.Add(new XElement("FIN_TYPE", zakSluch.FIN_TYPE));
        if (zakSluch.OPLATA > 0)
            Z_SL.Add(new XElement("OPLATA", RepeatedMEKs.Count() > 0 ? oplata_RepeatedMEKs?.OPLATA : zakSluch.OPLATA));
        if (zakSluch.SUMP != null)
            Z_SL.Add(new XElement("SUMP", RepeatedMEKs.Count() > 0 ? (zakSluch.SUMV - s_sum_RepeatedMEKs) : zakSluch.SUMP));
        if (zakSluch.SANK_IT > 0 || RepeatedMEKs.Count() > 0)
            Z_SL.Add(new XElement("SANK_IT", RepeatedMEKs.Count() > 0 ? s_sum_RepeatedMEKs : zakSluch.SANK_IT));

        return Z_SL;
    }
}