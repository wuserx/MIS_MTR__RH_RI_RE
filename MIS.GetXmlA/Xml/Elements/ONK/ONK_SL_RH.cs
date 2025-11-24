public class ONK_SL_RH
{
    private OnkSl _onksl;

    public ONK_SL_RH(OnkSl onksl)
    {
        _onksl = onksl;
    }

    public XElement Get()
    {
        IEnumerable<BDiag> bdiags = new RepositoryMIS(new MisContext()).GetAllBDiagByOnkSlIdAsync(_onksl.Id).Result;
        IEnumerable<BProt> bprots = new RepositoryMIS(new MisContext()).GetAllBProtByOnkSlIdAsync(_onksl.Id).Result;
        IEnumerable<OnkUsl> onkusls = new RepositoryMIS(new MisContext()).GetAllOnkUslByOnkSlIdAsync(_onksl.Id).Result;

        XElement ONKSL = new XElement("ONK_SL");

        if (_onksl.DS1_T != null)
            ONKSL.Add(new XElement("DS1_T", _onksl.DS1_T));
        if (_onksl.STAD != null)
            ONKSL.Add(new XElement("STAD", _onksl.STAD));
        if (_onksl.ONK_T != null)
            ONKSL.Add(new XElement("ONK_T", _onksl.ONK_T));
        if (_onksl.ONK_N != null)
            ONKSL.Add(new XElement("ONK_N", _onksl.ONK_N));
        if (_onksl.ONK_M != null)
            ONKSL.Add(new XElement("ONK_M", _onksl.ONK_M));
        if (_onksl?.MTSTZ > 0)
            ONKSL.Add(new XElement("MTSTZ", _onksl.MTSTZ));
        if (_onksl.SOD != null)
            ONKSL.Add(new XElement("SOD", _onksl.SOD));
        if (_onksl.K_FR != null)
            ONKSL.Add(new XElement("K_FR", _onksl.K_FR));
        if (_onksl.WEI != null)
            ONKSL.Add(new XElement("WEI", _onksl.WEI));
        if (_onksl.HEI != null)
            ONKSL.Add(new XElement("HEI", _onksl.HEI));
        if (_onksl.BSA != null)
            ONKSL.Add(new XElement("BSA", _onksl.BSA));

        foreach (var bdiag in bdiags)
        {
            if (bdiag != null)
                ONKSL.Add(new B_DIAG(bdiag).Get());
        }

        foreach (var bprot in bprots)
        {
            if (bprot != null)
                ONKSL.Add(new B_PROT(bprot).Get());
        }

        foreach (var onkusl in onkusls)
        {
            if (onkusl != null)
                ONKSL.Add(new ONK_USL_RH(onkusl).Get());
        }

        return ONKSL;
    }
}
