//XmlElement Пациент
using static System.Runtime.InteropServices.JavaScript.JSType;

public class PACIENT_RH
{ 
    public XElement Get(ZakSluch zakSluch)
    {
        XElement PACIENT = new XElement("PACIENT");

        if (zakSluch.ENP != null && zakSluch.VPOLIS >= 3)
            PACIENT.Add(new XElement("ENP", zakSluch?.ENP ));
        else if(zakSluch.qENP != null)
            PACIENT.Add(new XElement("ENP", zakSluch?.qENP));
        else
        {
            var enp = new RepositoryMIS(new MisContext()).GetEnpByPidAndZSluchDate2(zakSluch.Pid, zakSluch.DATE_Z_2).Result;
            
            if(!string.IsNullOrEmpty(enp?.ENP))
                PACIENT.Add(new XElement("ENP", enp.ENP));
            else
            {
                ////из ферзл
                //var FENPS = new Repoferzl3(zakSluch).GetPolicy().Result;

                //if (FENPS?.Count > 0)
                //    PACIENT.Add(new XElement("ENP", FENPS.MaxBy(m => m.pcyDateB).Enp));
            }
        }
            

        

        //else
        //{
        //    if (zakSluch.VPOLIS != null)
        //        PACIENT.Add(new XElement("VPOLIS", zakSluch.VPOLIS));
        //    if (zakSluch.SPOLIS != null)
        //        PACIENT.Add(new XElement("SPOLIS", zakSluch.SPOLIS));
        //    if (zakSluch.NPOLIS != null)
        //        PACIENT.Add(new XElement("NPOLIS", zakSluch.NPOLIS));
        //}

        if (zakSluch.ST_OKATO != null)
            PACIENT.Add(new XElement("ST_OKATO", zakSluch.ST_OKATO));
        if (zakSluch.SMO != null)
            PACIENT.Add(new XElement("SMO", zakSluch.SMO));
        if (zakSluch.INV != null)
            PACIENT.Add(new XElement("INV", zakSluch.INV));
        if (zakSluch.MSE != null)
            PACIENT.Add(new XElement("MSE", zakSluch.MSE)); 
        if (zakSluch.W != null)
            PACIENT.Add(new XElement("W", zakSluch.W));
        if (zakSluch.DR != null)
            PACIENT.Add(new XElement("DR", zakSluch.DR?.ToString("yyyy-MM-dd")));
        //if (zakSluch.DOCTYPE != null)
        //    PACIENT.Add(new XElement("DOCTYPE", zakSluch.DOCTYPE));
        //if (zakSluch.DOCSER != null)
        //    PACIENT.Add(new XElement("DOCSER", zakSluch.DOCSER));
        //if (zakSluch.DOCNUM != null)
        //    PACIENT.Add(new XElement("DOCNUM", zakSluch.DOCNUM));
        //if (zakSluch.SNILS != null)
        //    PACIENT.Add(new XElement("SNILS", zakSluch.SNILS));
        if (zakSluch.NOVOR != null)
            PACIENT.Add(new XElement("NOVOR", zakSluch.NOVOR));

        if (zakSluch.VNOV_D != null)
            PACIENT.Add(new XElement("VNOV_D", zakSluch.VNOV_D));

        if (zakSluch.SOC != null)
            PACIENT.Add(new XElement("SOC", zakSluch.SOC));

        if (zakSluch.COMMENTP != null)
            PACIENT.Add(new XElement("COMENTP", zakSluch.COMMENTP));



        return PACIENT;
    }
}