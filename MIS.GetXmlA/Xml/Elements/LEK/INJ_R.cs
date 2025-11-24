using MIS.Models.I;

public class INJ_R
{
    private INJ _inj;

    public INJ_R(INJ inj)
    {
        _inj = inj;
    }

    public XElement Get()
    {
        XElement INJ = new XElement("INJ");

        
        INJ.Add(new XElement("DATE_INJ", _inj.DATE_INJ.ToString("yyyy-MM-dd")));

        if (_inj.KV_INJ != null)
            INJ.Add(new XElement("KV_INJ", _inj.KV_INJ));

        if (_inj.KIZ_INJ != null)
            INJ.Add(new XElement("KIZ_INJ", _inj.KIZ_INJ));

        if (_inj.S_INJ != null)
            INJ.Add(new XElement("S_INJ", _inj.S_INJ));

        if (_inj.SV_INJ != null)
            INJ.Add(new XElement("SV_INJ", _inj.SV_INJ));

        if (_inj.SIZ_INJ != null)
            INJ.Add(new XElement("SIZ_INJ", _inj.SIZ_INJ));

        if (_inj.RED_INJ != null)
            INJ.Add(new XElement("RED_INJ", _inj.RED_INJ));


        return INJ;
    }
}