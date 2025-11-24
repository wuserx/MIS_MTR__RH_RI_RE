public class DS2_N
{
    private DS2N Ds2N;

    public DS2_N(DS2N ds2n)
    {
        Ds2N = ds2n;
    }

    public XElement Get()
    {
        XElement DS2N = new XElement("DS2_N");

        if (Ds2N.Ds2 != null)
            DS2N.Add(new XElement("DS2", Ds2N.Ds2));
        if (Ds2N.DS2_PR != null)
            DS2N.Add(new XElement("DS2_PR", Ds2N.DS2_PR));
        if (Ds2N.PR_DS2_N != null)
            DS2N.Add(new XElement("PR_DS2_N", Ds2N.PR_DS2_N)); 

        return DS2N;
    }
}
