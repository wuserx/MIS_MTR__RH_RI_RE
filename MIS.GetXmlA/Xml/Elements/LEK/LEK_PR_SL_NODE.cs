using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS_MTR_RH_RI_RE.GetXmlA.Xml.Elements.LEK
{
    public class LEK_PR_SL_NODE
    {

        public XElement GetNode(LekPrSl lekprsl, IEnumerable<LekDose> lekdoses)
        {
            XElement LEKPRSL = new XElement("LEK_PR");

            if (lekprsl.DATA_INJ != null)
                LEKPRSL.Add(new XElement("DATA_INJ", lekprsl.DATA_INJ?.ToString("yyyy-MM-dd")));
            if (lekprsl.CODE_SH != null)
                LEKPRSL.Add(new XElement("CODE_SH", lekprsl.CODE_SH));
            if (lekprsl.REGNUM != null)
                LEKPRSL.Add(new XElement("REGNUM", lekprsl.REGNUM));
            if (lekprsl.COD_MARK != null)
                LEKPRSL.Add(new XElement("COD_MARK", lekprsl.COD_MARK));

            foreach (var lekdose in lekdoses)
            {
                if (lekprsl != null)
                    LEKPRSL.Add(new LEK_DOSE(lekdose).Get());
            }

            return LEKPRSL;
        }
    }
}
