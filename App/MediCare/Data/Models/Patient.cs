using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MediCare.Data.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [MaxLength(20, ErrorMessage = "Imię może mieć maksymalnie 20 znaków.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [MaxLength(20, ErrorMessage = "Nazwisko może mieć maksymalnie 20 znaków.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "PESEL jest wymagany.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "PESEL musi składać się z 11 cyfr.")]
        public string Pesel { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane.")]
        [MaxLength(100, ErrorMessage = "Miasto może mieć maksymalnie 100 znaków.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Ulica jest wymagana.")]
        [MaxLength(100, ErrorMessage = "Ulica może mieć maksymalnie 100 znaków.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu.")]
        [MaxLength(20, ErrorMessage = "Numer telefonu może mieć maksymalnie 20 znaków.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        [DataType(DataType.Date, ErrorMessage = "Nieprawidłowy format daty.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Płeć jest wymagana.")]
        public GenderType Gender { get; set; }

        [Required]
        public int UserId { get; set; }

        public User user { get; set; }
      
    }
}





