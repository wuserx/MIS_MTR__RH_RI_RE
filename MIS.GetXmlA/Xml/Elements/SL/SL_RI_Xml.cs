//Сведения о случае
using System.Net;

public class SL_RI_Xml
{
    private Sluch_mtr _sluch;

    public SL_RI_Xml(Sluch_mtr sluch)
    {
        _sluch = sluch;
    }
    public XElement Get(ZakSluch_mtr zakSluch)
    {
        IEnumerable<KsgKpg_mtr> ksgkpgs = new RepositoryMTR(new MtrContext()).GetAllKsgKpgBySluchIdAsync(_sluch.Id).Result;
        IEnumerable<LekPrSl> lekprsls = new RepositoryMTR(new MtrContext()).GetAllLekPrSlBySluchIdAsync(_sluch.Id).Result;
        IEnumerable<Usl_mtr> usls = new RepositoryMTR(new MtrContext()).GetAllUslBySluchIdAsync(_sluch.Id).Result;
        IEnumerable<NApr> naprs = new RepositoryMTR(new MtrContext()).GetAllNAprBySluchIdAsync(_sluch.Id).Result;
        IEnumerable<COns> conss = new RepositoryMTR(new MtrContext()).GetAllCOnsBySluchIdAsync(_sluch.Id).Result;
        IEnumerable<OnkSl_mtr> onksls = new RepositoryMTR(new MtrContext()).GetAllOnkSlBySluchIdAsync(_sluch.Id).Result;
        IEnumerable<DS2N> ds2ns = new RepositoryMTR(new MtrContext()).GetAllDS2NBySluchIdAsync(_sluch.Id).Result;
        //IEnumerable<DS3N> ds3ns = new Repository(new AppContext()).GetAllDS3NBySluchIdAsync(Sluch.Id).Result;


        XElement SL = new XElement("SL");

        if (_sluch.SL_ID != null)
            SL.Add(new XElement("SL_ID", _sluch.SL_ID.Length < 20 ? string.Concat(_sluch.Id, "-", _sluch.SL_ID) : _sluch.SL_ID ));
        if (_sluch.VID_HMP != null)
            SL.Add(new XElement("VID_HMP", _sluch.VID_HMP));
        if (_sluch.METOD_HMP != null)
            SL.Add(new XElement("METOD_HMP", _sluch.METOD_HMP));
        if (_sluch.IDMODP_HMP != null)
            SL.Add(new XElement("IDMODP", _sluch.IDMODP_HMP));
        if (_sluch.PROFIL != null)
            SL.Add(new XElement("PROFIL", _sluch.PROFIL));
        else if (_sluch.PROFIL == null && _sluch.DET == 1)
            SL.Add(new XElement("PROFIL", 68));
        else if (_sluch.PROFIL == null && _sluch.DET == 0)
            SL.Add(new XElement("PROFIL", 97));
        if (_sluch.PROFIL_K != null)
            SL.Add(new XElement("PROFIL_K", _sluch.PROFIL_K));
        if (_sluch.DET != null)
            SL.Add(new XElement("DET", _sluch.DET));
        else
            if(new List<string> {"DS", "DU", "DF"}.Contains(zakSluch.FILENAME != null ? zakSluch.FILENAME[..2] : ""))
                SL.Add(new XElement("DET", 1));
            else
                SL.Add(new XElement("DET", 0));
        if (_sluch.P_CEL != null)
            SL.Add(new XElement("P_CEL", _sluch.P_CEL));
        else if (_sluch.P_CEL == null && (zakSluch.FILENAME != null ? Char.ToUpper(zakSluch.FILENAME[0]) : 'X') == 'D')
            SL.Add(new XElement("P_CEL", "2.2"));
        if (_sluch.DISP != null)
            SL.Add(new XElement("DISP", _sluch.DISP));
        if (_sluch.TAL_D != null)
            SL.Add(new XElement("TAL_D", _sluch.TAL_D?.ToString("yyyy-MM-dd")));
        if (_sluch.TAL_NUM != null)
            SL.Add(new XElement("TAL_NUM", _sluch.TAL_NUM));
        if (_sluch.TAL_P != null)
            SL.Add(new XElement("TAL_P", _sluch.TAL_P?.ToString("yyyy-MM-dd")));
        if (_sluch.NHISTORY != null)
            SL.Add(new XElement("NHISTORY", _sluch.NHISTORY));
        if (_sluch.P_PER != null)
            SL.Add(new XElement("P_PER", _sluch.P_PER));
        if (_sluch.LPU_1 != null)
            SL.Add(new XElement("DATE_1", _sluch.DATE_1?.ToString("yyyy-MM-dd")));
        if (_sluch.DATE_2 != null)
            SL.Add(new XElement("DATE_2", _sluch.DATE_2?.ToString("yyyy-MM-dd")));
        if (_sluch.KD != null)
            SL.Add(new XElement("KD", _sluch.KD));
        if (_sluch.WEI != null)
            SL.Add(new XElement("WEI", _sluch.WEI));
        if (_sluch.DS0 != null)
            SL.Add(new XElement("DS0", _sluch.DS0));
        if (_sluch.DS1 != null)
            SL.Add(new XElement("DS1", _sluch.DS1));
        if (_sluch.DS1_PR != null)
            SL.Add(new XElement("DS1_PR", _sluch.DS1_PR));
        foreach (var ds2n in ds2ns)
        {
            if (ds2n != null)
                SL.Add(new DS2_N(ds2n).Get());
        }
        //foreach (var ds3n in ds3ns)
        //{
        //    if (ds3n != null)
        //        SL.Add(new DS3_N(ds3n).Get());
        //}
        if (_sluch.C_ZAB != null)
            SL.Add(new XElement("C_ZAB", _sluch.C_ZAB));
        if (_sluch.DS_ONK != null)
            SL.Add(new XElement("DS_ONK", _sluch.DS_ONK));
        if (_sluch.DN != null)
            SL.Add(new XElement("DN", _sluch.DN));
        if (_sluch.DN_NEXT_DT != null)
            SL.Add(new XElement("DN_NEXT_DT", _sluch.DN_NEXT_DT));
        if (_sluch.CODE_MES1s != null)
            SL.Add(new XElement("CODE_MES1", _sluch.CODE_MES1s));
        if (_sluch.CODE_MES2 != null)
            SL.Add(new XElement("CODE_MES2", _sluch.CODE_MES2));
        foreach (var napr in naprs)
        {
            if (napr != null)
                SL.Add(new NAPR(napr).Get());
        }        

        if (conss.Count() > 0)
        {
            foreach (var cons in conss)
            {
                if (cons != null)
                    SL.Add(new CONS(cons).Get());
            }
        }
        else
        {
            /*Обязательно к заполнению при подозрении на злокачественное новообразование (DS_ONK=1) 
             * или установленном диагнозе злокачественного новообразования (первый символ кода основного диагноза - «С» 
             * или код основного диагноза входит в диапазон D00-D09) и нейтропении (код основного диагноза - D70 с сопутствующим  диагнозом C00-C80 или C97). 
             * При отсутствии подозрения на злокачественное новообразование 
             * и установленного диагноза злокачественного новообразования, 
             * а также для случаев, первоначально поданных в соответствии с пунктом Д.3 
             * Приложения Д (DISP=1), заполнению не подлежит.*/
            if ((_sluch.DS_ONK == 1

                || _sluch?.DS1?.StartsWith("C") == true

                || (new Regex(@"D0[0-9]").IsMatch(_sluch?.DS1) == true && (_sluch?.DS1 == "D70"
                        && _sluch?._Ds2?.Any(item => new Regex(@"C[0-8]0").IsMatch(item)) == true || _sluch?._Ds2?.Any(item => new Regex("C97").IsMatch(item)) == true)
                   )
                )
                && _sluch.DISP != 1
               )
            {
                SL.Add(new XElement("CONS", new XElement("PR_CONS", 0)));
            }
        }

        foreach (var onksl in onksls)
        {
            if (onksl != null)
                SL.Add(new ONK_SL_RI(onksl).Get());
        }
        foreach (var ksgkpg in ksgkpgs)
        {
            if (ksgkpg != null)
                SL.Add(new KSG_KPG_RI(ksgkpg).Get());
        }
        if (_sluch.REAB != null)
            SL.Add(new XElement("REAB", 1));
        if (_sluch.PRVS != null)
            SL.Add(new XElement("PRVS", _sluch.PRVS));
        if (_sluch.VERS_SPEC != null)
            SL.Add(new XElement("VERS_SPEC", _sluch.VERS_SPEC));

                    
        if (_sluch.IDDOKT != null)
            SL.Add(new XElement("IDDOKT", _sluch.IDDOKT));
        else
            SL.Add(new XElement("IDDOKT", _sluch.PODR));            
           

        if (_sluch.TYPE_AMB != null)
            SL.Add(new XElement("TYPE_AMB", _sluch.TYPE_AMB));
        if (_sluch.REAB >= 1 && _sluch.DATE_2?.Year >= 2023)
            SL.Add(new XElement("REAB_SCORE", _sluch.REAB));
        if (_sluch.ED_COL != null)
            SL.Add(new XElement("ED_COL", _sluch.ED_COL));
        if (_sluch.TARIF != null)
            SL.Add(new XElement("TARIF", _sluch.TARIF));
        if (_sluch.SUM_M != null)
            SL.Add(new XElement("SUM_M", _sluch.SUM_M));
        foreach (var lekprsl in lekprsls)
        {
            if (lekprsl != null)
                SL.Add(new LEK_PR_SL_RI(lekprsl).Get());
        }

        foreach (var (usl, usl_index) in usls.Select((v, i) => (v, i)))
        {
            if (usl != null)
                if (Char.ToUpper(usl.CODE_USL != null ? usl.CODE_USL[0] : 'X') != 'D')
                    SL.Add(new USL_RI_Xml(usl).Get(zakSluch.USL_OK, usl_index, _sluch.IDDOKT, zakSluch.FILENAME));
        }

        if (new List<int>() { 3, 4 }.Contains(zakSluch.USL_OK) && usls.Count() <= 0)
            SL.Add(new USL_RI_Xml().GetUslNoDb(zakSluch, _sluch));

        if (_sluch.COMENTSL != null)
            SL.Add(new XElement("COMENTSL", _sluch.COMENTSL));

        return SL;
    }
}