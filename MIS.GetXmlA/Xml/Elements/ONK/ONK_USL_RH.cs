public class ONK_USL_RH
{
    private OnkUsl _onkusl;

    public ONK_USL_RH(OnkUsl onkusl)
    {
        _onkusl = onkusl;
    }

    public XElement Get()
    {
        IEnumerable<LEkPr> lekprs = new RepositoryMIS(new MisContext()).GetAllLEkPrByOnkUslIdAsync(_onkusl.Id).Result;

        XElement ONKUSL = new XElement("ONK_USL");

        if (_onkusl.USL_TIP > 0)
            ONKUSL.Add(new XElement("USL_TIP", _onkusl.USL_TIP));
        if (_onkusl.HIR_TIP != null)
            ONKUSL.Add(new XElement("HIR_TIP", _onkusl.HIR_TIP));
        if (_onkusl.LEK_TIP_L != null)
            ONKUSL.Add(new XElement("LEK_TIP_L", _onkusl.LEK_TIP_L));
        if (_onkusl.LEK_TIP_V != null)
            ONKUSL.Add(new XElement("LEK_TIP_V", _onkusl.LEK_TIP_V));

        if(lekprs.Count() > 0)
        {
            foreach (var lekpr in lekprs)
            {
                if (lekpr != null)
                    ONKUSL.Add(new LEK_PR_RH(lekpr).Get());
            }
        }
        else if (_onkusl?.LekPrs?.Count() > 0)
        {
            foreach(var lekpr in _onkusl.LekPrs)
            {
                ONKUSL.Add(new LEK_PR_RH(lekpr).Get());
            }            
        }


        if (_onkusl?.PPTR > 0)
            ONKUSL.Add(new XElement("PPTR", _onkusl.PPTR));
        if (_onkusl.LUCH_TIP != null)
            ONKUSL.Add(new XElement("LUCH_TIP", _onkusl.LUCH_TIP));

        return ONKUSL;
    }
}