public class EXP_ZAP_RI
{    
    public XElement Get(Schet_mtr schet, ZakSluch_mtr zakSluch)
    {
        XElement EXP_ZAP = new XElement("EXP_ZAP");

        var Z_SL = new Z_SL_RI_RE().Get(zakSluch);

        if (Z_SL.Elements().Count() > 0)
        {
            if (schet != null)
                EXP_ZAP.Add(new XElement("N_EXP_ZAP", string.Concat(new RepositoryMTR(new MtrContext()).GetSchetCODE(schet)), "-", zakSluch.Id));
            if (schet != null)
                EXP_ZAP.Add(new XElement("CODE_SCHET", new RepositoryMTR(new MtrContext()).GetSchetCODE(schet)));

            EXP_ZAP.Add(new XElement("N_ZAP", new RepositoryMTR(new MtrContext()).GetZapN_ZAP(zakSluch)));

            EXP_ZAP.Add(Z_SL);
        }

            

        return EXP_ZAP;
    }
}
