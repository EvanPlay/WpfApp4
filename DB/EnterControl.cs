using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.DB
{
    public class EnterControl
    {
        [Key]
        public int EnterControlId { get; set; }
        public DateTime DateTimeEnterControlId { get; set; }
        public int AcauntId { get; set; }
        public Acaunt Acaunt { get; set; }
    }
}
