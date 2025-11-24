public class B_DIAG
{
    private BDiag Ddiag;

    public B_DIAG(BDiag bdiag)
    {
        Ddiag = bdiag;
    }

    public XElement Get()
    {
        XElement BDIAG = new XElement("B_DIAG");

        if (Ddiag.DIAG_DATE != null)
            BDIAG.Add(new XElement("DIAG_DATE", Ddiag.DIAG_DATE?.ToString("yyyy-MM-dd")));
        if (Ddiag.DIAG_TIP != null)
            BDIAG.Add(new XElement("DIAG_TIP", Ddiag.DIAG_TIP));
        if (Ddiag.DIAG_CODE != null)
            BDIAG.Add(new XElement("DIAG_CODE", Ddiag.DIAG_CODE));
        if (Ddiag.DIAG_RSLT != null)
            BDIAG.Add(new XElement("DIAG_RSLT", Ddiag.DIAG_RSLT));
        if (Ddiag.REC_RSLT != null)
            BDIAG.Add(new XElement("REC_RSLT", Ddiag.REC_RSLT));

        return BDIAG;
    }
}
