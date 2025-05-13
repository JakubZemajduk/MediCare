using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MediCare.Data.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa specjalizacji jest wymagana.")]
        [MaxLength(100, ErrorMessage = "Nazwa może mieć maksymalnie 100 znaków.")]
        public string Name { get; set; }
    }
}
