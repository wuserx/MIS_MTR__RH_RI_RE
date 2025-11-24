using MIS.Models.I;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS_MTR_RH_RI_RE.GetXmlA.Xml.Elements.LEK
{
    public class LEK_PR_NODE
    {
        public XElement GetNode(LEkPr _lekpr, LekPr2 _lekpr2, IEnumerable<INJ> injs)
        {
            XElement LEKPR = new XElement("LEK_PR");

            if (_lekpr?.REGNUM != null)
                LEKPR.Add(new XElement("REGNUM", _lekpr.REGNUM));
            else if (_lekpr2.REGNUM != null)
                LEKPR.Add(new XElement("REGNUM", _lekpr2.REGNUM));

            if (_lekpr?.REGNUM_DOP != null)
                LEKPR.Add(new XElement("REGNUM_DOP", _lekpr.REGNUM_DOP));

            if (_lekpr?.CODE_SH != null)
                LEKPR.Add(new XElement("CODE_SH", _lekpr.CODE_SH));
            else if (_lekpr2?.CODE_SH != null)
                LEKPR.Add(new XElement("CODE_SH", _lekpr2.CODE_SH));

            if (injs.Count() > 0)
            {
                foreach (var inj in injs)
                {
                    LEKPR.Add(new INJ_R(inj).Get());
                }
            }
            else //эта часть нужна только для Т файлов
            {
                if (_lekpr?.date_injs?.Count() > 0) //далеко прокидывать: && Char.ToUpper(ctxSCHET.FILENAME[0]) == 'T')
                {
                    foreach (var date_inj in _lekpr.date_injs)
                    {
                        if (Init.YEAR_REPORT <= 2024)
                        {
                            LEKPR.Add(new XElement("DATE_INJ", date_inj.ToString("yyyy-MM-dd")));
                        }
                        else
                        {
                            XElement INJ = new XElement("INJ");

                            INJ.Add(new XElement("DATE_INJ", date_inj.ToString("yyyy-MM-dd")));

                            LEKPR.Add(INJ);
                        }
                    }
                }
                else if (_lekpr2?.date_injs?.Count() > 0)
                {
                    foreach (var date_inj in _lekpr2.date_injs)
                    {
                        if (Init.YEAR_REPORT <= 2024)
                        {
                            LEKPR.Add(new XElement("DATE_INJ", date_inj.ToString("yyyy-MM-dd")));
                        }
                        else
                        {
                            XElement INJ = new XElement("INJ");

                            INJ.Add(new XElement("DATE_INJ", date_inj.ToString("yyyy-MM-dd")));

                            LEKPR.Add(INJ);
                        }
                    }
                }
            }

            return LEKPR;
        }
    }
}
