public class MR_USL_N_RH
{
    private MrUslN MrUsln;

    public MR_USL_N_RH(MrUslN mrUsln)
    {
        MrUsln = mrUsln;
    }

    public XElement Get(Usl usl, int index, string iddokt)
    {
        XElement MRUSLN = new XElement("MR_USL_N");

        if (MrUsln != null)
        {
            if (MrUsln.MR_N != null)
                MRUSLN.Add(new XElement("MR_N", MrUsln.MR_N));
        }            
        else
        {
            if (index > -1)
                MRUSLN.Add(new XElement("MR_N", ++index));
        }
            

        if (MrUsln != null)
        {
            if (MrUsln.PRVS != null)
                MRUSLN.Add(new XElement("PRVS", MrUsln.PRVS));
        }            
        else
        {
            if (usl.PRVS != null)
                MRUSLN.Add(new XElement("PRVS", usl.PRVS));
        }
            

        if (MrUsln != null)
        {
            if (MrUsln.CODE_MD != null)
                MRUSLN.Add(new XElement("CODE_MD", MrUsln.CODE_MD));
        }            
        else
        {
            if (usl.CODE_MD != null)
                MRUSLN.Add(new XElement("CODE_MD", usl.CODE_MD));
            else if(iddokt != null && iddokt != "")
            {
                MRUSLN.Add(new XElement("CODE_MD", iddokt));
            }
            else
            {
                MRUSLN.Add(new XElement("CODE_MD", usl.PODR));
            }
                
        }
            

        return MRUSLN;
    }
}