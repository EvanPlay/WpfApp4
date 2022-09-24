using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.DB
{
    public class Acaunt
    {
        [Key]
        public int AcauntId { get; set; }
        public string AcauntName { get; set; }
        public string PathImage { get; set; }
    }
}
