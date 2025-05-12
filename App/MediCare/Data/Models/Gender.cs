using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MediCare.Data.Models
{
    public class Gender
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
    }
}
