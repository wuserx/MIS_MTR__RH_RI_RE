//XmlElement Пациент
public class PACIENT_RI
{ 
    public XElement Get(ZakSluch_mtr zakSluch)
    {
        XElement PACIENT = new XElement("PACIENT");

        
        if (zakSluch.ENP != null && zakSluch.VPOLIS >= 3)
            PACIENT.Add(new XElement("ENP", zakSluch?.ENP ?? zakSluch?.NPOLIS));
        else
        {
            ////ferzl
            //var FENPS = new Repoferzl3(zakSluch).GetPolicy().Result;

            //if (FENPS?.Count > 0)
            //    PACIENT.Add(new XElement("ENP", FENPS.MaxBy(m => m.pcyDateB).Enp));


        }

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