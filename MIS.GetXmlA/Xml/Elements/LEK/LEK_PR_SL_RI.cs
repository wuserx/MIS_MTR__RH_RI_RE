using MIS_MTR_RH_RI_RE.GetXmlA.Xml.Elements.LEK;
using System.Collections.Generic;

public class LEK_PR_SL_RI
{
    private LekPrSl lekprsl;

    public LEK_PR_SL_RI(LekPrSl lekPrSl)
    {
        lekprsl = lekPrSl;
    }

    public XElement Get()
    {
        IEnumerable<LekDose> lekdoses = new RepositoryMTR(new MtrContext()).GetAllLekDoseByLekPrSlIdAsync(lekprsl.Id).Result;       

        return new LEK_PR_SL_NODE().GetNode(lekprsl, lekdoses);
    }
}
