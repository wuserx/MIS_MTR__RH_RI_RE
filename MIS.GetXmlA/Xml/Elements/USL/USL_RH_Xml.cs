using MIS_MTR_RH_RI_RE.GetXmlA.Models.I;

public class USL_RH_Xml
{
    private Usl _usl;

    public USL_RH_Xml(Usl usl)
    {
        _usl = usl;
    }

    public USL_RH_Xml()
    {
    }

    public XElement Get(ZakSluch zakSluch, int usl_index, Sluch sluch)
    {         
        IEnumerable<MedDev> meddevs = new RepositoryMIS(new MisContext()).GetAllMedDevByUslIdAsync(_usl.Id).Result;
        IEnumerable<MrUslN> mruslns = new RepositoryMIS(new MisContext()).GetAllMrUslNByUslIdAsync(_usl.Id).Result;

        XElement USL = new XElement("USL");

        if (_usl.IDSERV != null)
            USL.Add(new XElement("IDSERV", new RepositoryMIS(new MisContext()).GetUslIDSERV(_usl)));
        if (_usl.LPU != null)
            USL.Add(new XElement("LPU", _usl.LPU));       
        if (_usl.PROFIL != null)
            USL.Add(new XElement("PROFIL", _usl.PROFIL));
        else if (_usl.PROFIL == null && _usl.DET == 1)
            USL.Add(new XElement("PROFIL", 68));
        else if (_usl.PROFIL == null && _usl.DET == 0)
            USL.Add(new XElement("PROFIL", 97));

        if (_usl.VID_VME != null && zakSluch.VIDPOM == 32)
            USL.Add(new XElement("VID_VME", _usl.VID_VME));
        else if(_usl.VID_VME != null)
            USL.Add(new XElement("VID_VME", string.Join(".", _usl.VID_VME.Split('.').Take(3)) ));
        
            

        if (_usl.DET != null)
            USL.Add(new XElement("DET", _usl.DET));
        else
            if (new List<string> { "DS", "DU", "DF" }.Contains(zakSluch.FILENAME != null ? zakSluch.FILENAME[..2] : ""))
            USL.Add(new XElement("DET", 1));
        else
            USL.Add(new XElement("DET", 0));

        if (_usl.DATE_IN != null)
            USL.Add(new XElement("DATE_IN", _usl.DATE_IN.ToString("yyyy-MM-dd")));
        if (_usl.DATE_OUT != null)
            USL.Add(new XElement("DATE_OUT", _usl.DATE_OUT.ToString("yyyy-MM-dd")));

        if (_usl.DS != null && Init.NO_CREATE_DS_FIELD_IN_USL_BY_DATA?.Contains(_usl.DS) == false)
            USL.Add(new XElement("DS", _usl.DS));

        if (_usl.CODE_USL_UNLOAD != null)
            USL.Add(new XElement("CODE_USL", _usl.CODE_USL_UNLOAD));
        else
            USL.Add(new XElement("CODE_USL", _usl.CODE_USL));

        if (_usl.USL != null)
            USL.Add(new XElement("USL", _usl.USL));
        else
            USL.Add(new XElement("USL", _usl.CODE_USL_UNLOAD));

        if (_usl.KOL_USL != null)
            USL.Add(new XElement("KOL_USL", _usl.KOL_USL));
        USL.Add(new XElement("TARIF", _usl.TARIF != null ? _usl.TARIF : 0));
        if (_usl.SUMV_USL != null)
            USL.Add(new XElement("SUMV_USL", _usl.SUMV_USL));
        if ((new[] { 3, 4 }).Contains(zakSluch.USL_OK) && _usl.CODE_UA != null)
            USL.Add(new XElement("CODE_UA", _usl.CODE_UA));
        if ((new [] { 3, 4 }).Contains( zakSluch.USL_OK ) && _usl.USL_LEVEL != null)
            USL.Add(new XElement("USL_LEVEL", _usl.USL_LEVEL));
        foreach (var meddev in meddevs)
        {
            if (meddev != null)
                USL.Add(new MED_DEV(meddev).Get());
        }

        if (_usl.P_OTK != 1)
        {
            if (mruslns == null)
                foreach (var (mrusln, mrusln_index) in mruslns.Select((v, i) => (v, i)))
                {
                    USL.Add(new MR_USL_N_RH(mrusln).Get(_usl, mrusln_index, sluch.IDDOKT));
                }
            else
                USL.Add(new MR_USL_N_RH(mruslns.FirstOrDefault()).Get(_usl, usl_index, sluch.IDDOKT));
        }
        

        if (_usl.COMENTU != null)
            USL.Add(new XElement("COMENTU", _usl.COMENTU));

        return USL;
    }

    //этот блок для USL отсут.х в БД (рисуем блок USL)
    public XElement GetUslNoDb(ZakSluch zakSluch, Sluch sluch)
    {
        XElement USL = new XElement("USL");

        var code_ua = new RepositoryMIS(new MisContext()).GetAMB_USL_PROFIL(zakSluch, sluch).Result;

        if (sluch.PROFIL == null)
        {
            if (sluch.DET == 1)
                code_ua = "B01.031.001";
            else
                code_ua = "B01.047.001";
        }


        USL.Add(new XElement("IDSERV", new RepositoryMIS(new MisContext()).GetZakSluchIDCASE(zakSluch)) );
        
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