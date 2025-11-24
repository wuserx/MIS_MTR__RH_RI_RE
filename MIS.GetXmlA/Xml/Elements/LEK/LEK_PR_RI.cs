using MIS.Models.I;
using MIS_MTR_RH_RI_RE.GetXmlA.Xml.Elements.LEK;
using System.Collections.Generic;

public class LEK_PR_RI
{
    private LEkPr _lekpr;
    private LekPr2 _lekpr2;

    public LEK_PR_RI(LEkPr lekpr)
    {
        _lekpr = lekpr;
    }

    public LEK_PR_RI(LekPr2 lekpr2)
    {
        _lekpr2 = lekpr2;
    }

    public XElement Get()
    {
        IEnumerable<INJ> injs = _lekpr != null
        ? new RepositoryMTR(new MtrContext()).GetAllINJByLekPrIdAsync(_lekpr.Id).Result
        : Enumerable.Empty<INJ>();

        return new LEK_PR_NODE().GetNode(_lekpr, _lekpr2, injs);
    }
}