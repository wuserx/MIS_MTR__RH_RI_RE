public class EXP_ZAP_RH
{    
    public XElement Get(Schet schet, ZakSluch zakSluch)
    {
        XElement EXP_ZAP = new XElement("EXP_ZAP");

        //Console.WriteLine($"Счёт {schet.FILENAME_PACKET} {DateTime.Now.ToString("yy.MM.dd - HH:mm:ss")}");

        var Z_SL = new Z_SL_RH_RE().Get(zakSluch);

        if(Z_SL.Elements().Count() > 0)
        {
            if (schet != null)
                EXP_ZAP.Add(new XElement("N_EXP_ZAP", string.Concat(new RepositoryMIS(new MisContext()).GetSchetCODE(schet)), "-", zakSluch.Id));
            if (schet != null)
                EXP_ZAP.Add(new XElement("CODE_SCHET", new RepositoryMIS(new MisContext()).GetSchetCODE(schet)));

            EXP_ZAP.Add(new XElement("N_ZAP", new RepositoryMIS(new MisContext()).GetZapN_ZAP(zakSluch)));

            EXP_ZAP.Add(Z_SL);
        }        

        return EXP_ZAP;
    }
}
