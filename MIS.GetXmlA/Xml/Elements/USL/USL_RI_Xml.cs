public class USL_RI_Xml
{
    private Usl_mtr Usl;

    public USL_RI_Xml(Usl_mtr usl)
    {
        Usl = usl;
    }

    public USL_RI_Xml()
    {
        
    }

    public XElement Get(int USL_OK, int usl_index, string? iddokt, string? FILENAME)
    {         
        IEnumerable<MedDev> meddevs = new RepositoryMTR(new MtrContext()).GetAllMedDevByUslIdAsync(Usl.Id).Result;
        IEnumerable<MrUslN> mruslns = new RepositoryMTR(new MtrContext()).GetAllMrUslNByUslIdAsync(Usl.Id).Result;

        XElement USL = new XElement("USL");

        if (Usl.IDSERV != null)
            USL.Add(new XElement("IDSERV", Usl.IDSERV.Length < 20 ? string.Concat(Usl.Id, "-", Usl.IDSERV) : Usl.IDSERV));
        if (Usl.LPU != null)
            USL.Add(new XElement("LPU", Usl.LPU));       
        if (Usl.PROFIL != null)
            USL.Add(new XElement("PROFIL", Usl.PROFIL));
        else if (Usl.PROFIL == null && Usl.DET == 1)
            USL.Add(new XElement("PROFIL", 68));
        else if (Usl.PROFIL == null && Usl.DET == 0)
            USL.Add(new XElement("PROFIL", 97));
        if (Usl.VID_VME != null)
            USL.Add(new XElement("VID_VME", Usl.VID_VME));
        if(Usl.DET != null)
            USL.Add(new XElement("DET", Usl.DET));
        else
            if (new List<string> { "DS", "DU", "DF" }.Contains(FILENAME != null ? FILENAME[..2] : ""))
                USL.Add(new XElement("DET", 1));
            else
                USL.Add(new XElement("DET", 0));
        if (Usl.DATE_IN != null)
            USL.Add(new XElement("DATE_IN", Usl.DATE_IN.ToString("yyyy-MM-dd")));
        if (Usl.DATE_OUT != null)
            USL.Add(new XElement("DATE_OUT", Usl.DATE_OUT.ToString("yyyy-MM-dd")));

        if (Usl.DS != null && Init.NO_CREATE_DS_FIELD_IN_USL_BY_DATA?.Contains(Usl.DS) == false)
            USL.Add(new XElement("DS", Usl.DS));

        if (Usl.CODE_USL_UNLOAD != null)
            USL.Add(new XElement("CODE_USL", Usl.CODE_USL_UNLOAD));
        else
            USL.Add(new XElement("CODE_USL", Usl.CODE_USL));

        if (Usl.USL != null)
            USL.Add(new XElement("USL", Usl.USL));
        else
            USL.Add(new XElement("USL", Usl.CODE_USL_UNLOAD));

        if (Usl.KOL_USL != null)
            USL.Add(new XElement("KOL_USL", Usl.KOL_USL));
        USL.Add(new XElement("TARIF", Usl.TARIF != null ? Usl.TARIF : 0));
        if (Usl.SUMV_USL != null)
            USL.Add(new XElement("SUMV_USL", Usl.SUMV_USL));
        if ((new[] { 3, 4 }).Contains(USL_OK) && Usl.CODE_UA != null)
            USL.Add(new XElement("CODE_UA", Usl.CODE_UA));
        if ( (new [] { 3, 4 }).Contains( USL_OK ) && Usl.USL_LEVEL != null)
            USL.Add(new XElement("USL_LEVEL", Usl.USL_LEVEL));
        foreach (var meddev in meddevs)
        {
            if (meddev != null)
                USL.Add(new MED_DEV(meddev).Get());
        }

        //USL.Add(new XElement("SluchId", Usl.SluchId));//для проверки - закомментировать

        if (Usl.P_OTK != 1)
        {
            if (mruslns == null)
                foreach (var (mrusln, mrusln_index) in mruslns.Select((v, i) => (v, i)))
                {
                    USL.Add(new MR_USL_N_RI(mrusln).Get(Usl, mrusln_index, iddokt));
                }
            else
                USL.Add(new MR_USL_N_RI(mruslns.FirstOrDefault()).Get(Usl, usl_index, iddokt));
        }
        

        if (Usl.COMENTU != null)
            USL.Add(new XElement("COMENTU", Usl.COMENTU));

        return USL;
    }

    //этот блок для USL отсут.х в БД (рисуем блок USL)
    public XElement GetUslNoDb(ZakSluch_mtr zakSluch, Sluch_mtr sluch)
    {
        XElement USL = new XElement("USL");

        var code_ua = new RepositoryMTR(new MtrContext()).GetAMB_USL_PROFIL(zakSluch, sluch).Result;

        if (sluch.PROFIL == null)
        {
            if (sluch.DET == 1)
                code_ua = "B01.031.001";
            else
                code_ua = "B01.047.001";
        }


        USL.Add(new XElement("IDSERV", new RepositoryMTR(new MtrContext()).GetZakSluchIDCASE(zakSluch)));

        USL.Add(new XElement("LPU", zakSluch.LPU));

        if (sluch.PROFIL != null)
            USL.Add(new XElement("PROFIL", sluch.PROFIL));

        if (sluch.DET != null)
            USL.Add(new XElement("DET", sluch.DET));
        else
            if (new List<string> { "DS", "DU", "DF" }.Contains(zakSluch.FILENAME != null ? zakSluch.FILENAME[..2] : ""))
            USL.Add(new XElement("DET", 1));
        else
            USL.Add(new XElement("DET", 0));

        USL.Add(new XElement("DATE_IN", zakSluch.DATE_Z_1?.ToString("yyyy-MM-dd")));

        USL.Add(new XElement("DATE_OUT", zakSluch.DATE_Z_2?.ToString("yyyy-MM-dd")));

        if (sluch.DS1 != null && Init.NO_CREATE_DS_FIELD_IN_USL_BY_DATA?.Contains(sluch.DS1) == false)
            USL.Add(new XElement("DS", sluch.DS1));

        if (code_ua != null)
            USL.Add(new XElement("CODE_USL", code_ua));

        USL.Add(new XElement("KOL_USL", 1));

        USL.Add(new XElement("SUMV_USL", 0));

        if (code_ua != null)
            USL.Add(new XElement("CODE_UA", code_ua));


        USL.Add(new XElement("USL_LEVEL", 1));

        return USL;
    }
}