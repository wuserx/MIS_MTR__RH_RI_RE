public class B_PROT
{
    private BProt Bprot;

    public B_PROT(BProt bprot)
    {
        Bprot = bprot;
    }

    public XElement Get()
    {
        XElement BPROT = new XElement("B_PROT");

        if (Bprot.PROT != null)
            BPROT.Add(new XElement("PROT", Bprot.PROT));
        if (Bprot.D_PROT != null)
            BPROT.Add(new XElement("D_PROT", Bprot.D_PROT?.ToString("yyyy-MM-dd")));

        return BPROT;
    }
}
