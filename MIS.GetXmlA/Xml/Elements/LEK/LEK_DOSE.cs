public class LEK_DOSE
{
    private LekDose lekDose;

    public LEK_DOSE(LekDose lekdose)
    {
        lekDose = lekdose;
    }

    public XElement Get()
    {
        XElement LEKDOSE = new XElement("LEK_DOSE");

        if (lekDose.ED_IZM != null)
            LEKDOSE.Add(new XElement("ED_IZM", lekDose.ED_IZM));
        if (lekDose.DOSE_INJ > 0)
            LEKDOSE.Add(new XElement("DOSE_INJ", lekDose.DOSE_INJ));
        if (lekDose.METHOD_INJ != null)
            LEKDOSE.Add(new XElement("METHOD_INJ", lekDose.METHOD_INJ));
        if (lekDose.COL_INJ > 0)
            LEKDOSE.Add(new XElement("COL_INJ", lekDose.COL_INJ));

        return LEKDOSE;
    }
}