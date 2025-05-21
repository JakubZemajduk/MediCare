using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MediCare.Data.Models
{
    public enum GenderType
    {
        [Display(Name = "Mężczyzna")]
        Male = 1,

        [Display(Name = "Kobieta")]
        Female = 2
    }

}
