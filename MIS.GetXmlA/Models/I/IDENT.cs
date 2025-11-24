using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS_MTR_RH_RI_RE.GetXmlA.Models.I
{
    public class IDENT
    {
        [Key]
        public int ID { get; set; } 
        public SCHET Schet{get; set;}
    }

    public class SCHET
    {
        [Key]
        public string? CODE { get; set; }
        public int YEAR { get; set; }
        public int MONTH { get; set; }
        public List<ZAP> ZAP = new List<ZAP>();
    }

    public class ZAP
    {
        [Key]
        public string N_ZAP { get; set; }

        public List<Z_SL> Z_SL = new List<Z_SL>();
    }

    public class Z_SL
    {
        [Key]
        public string IDCASE { get; set; }

        public List<SL> SL = new List<SL>();
    }

    public class SL
    {
        [Key]
        public string SL_ID { get; set; }
    }
}
