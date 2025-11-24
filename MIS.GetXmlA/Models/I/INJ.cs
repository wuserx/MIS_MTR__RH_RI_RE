using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Models.I
{
    [Table("INJ", Schema = "Reestr")]
    public class INJ
    {
        public int Id { get; set; }
        public DateTime DATE_INJ { get; set; }
        public decimal? KV_INJ { get; set; }
        public decimal? KIZ_INJ { get; set; }
        public decimal? S_INJ { get; set; }
        public decimal? SV_INJ { get; set; }
        public decimal? SIZ_INJ { get; set; }
        public int? RED_INJ { get; set; }


        [Required]
        public int LEkPrId { get; set; }

        [ForeignKey("LEkPrId")]
        public virtual LEkPr LEkPr { get; set; }
    }
}
