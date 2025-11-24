public class CONS
{
    private COns Cons;

    public CONS(COns cons)
    {
        Cons = cons;
    }

    public XElement Get()
    {
        XElement CONS = new XElement("CONS");

        if (Cons.PR_CONS != null)
            CONS.Add(new XElement("PR_CONS", Cons.PR_CONS));
        if (Cons.DT_CONS != null)
            CONS.Add(new XElement("DT_CONS", Cons.DT_CONS?.ToString("yyyy-MM-dd")));
        return CONS;
    }
}