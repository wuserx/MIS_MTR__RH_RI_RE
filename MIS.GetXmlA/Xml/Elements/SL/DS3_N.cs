public class DS3_N
{
    private DS3N Ds3N;

    public DS3_N(DS3N ds3n)
    {
        Ds3N = ds3n;
    }

    public XElement Get()
    {
        XElement DS3N = new XElement("DS3_N");

        if (Ds3N.Ds3 != null)
            DS3N.Add(new XElement("DS3", Ds3N.Ds3));
        if (Ds3N.DS3_PR != null)
            DS3N.Add(new XElement("DS3_PR", Ds3N.DS3_PR));
        if (Ds3N.PR_DS3_N != null)
            DS3N.Add(new XElement("PR_DS3_N", Ds3N.PR_DS3_N)); 

        return DS3N;
    }
}
