public class MED_DEV
{
    private MedDev Meddev;

    public MED_DEV(MedDev meddev)
    {
        Meddev = meddev;
    }

    public XElement Get()
    {
        XElement MEDDEV = new XElement("MED_DEV");

        if (Meddev.DATE_MED != null)
            MEDDEV.Add(new XElement("DATE_MED", Meddev.DATE_MED?.ToString("yyyy-MM-dd")));
        if (Meddev.CODE_MEDDEV != null)
            MEDDEV.Add(new XElement("CODE_MEDDEV", Meddev.CODE_MEDDEV));
        if (Meddev.NUMBER_SER != null)
            MEDDEV.Add(new XElement("NUMBER_SER", Meddev.NUMBER_SER));
        return MEDDEV;
    }
}