using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MediCare.Data.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [MaxLength(20, ErrorMessage = "Imię może mieć maksymalnie 20 znaków.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [MaxLength(20, ErrorMessage = "Nazwisko może mieć maksymalnie 20 znaków.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [MaxLength(20, ErrorMessage = "Numer telefonu może mieć maksymalnie 20 znaków.")]
        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Specjalizacja jest wymagana.")]
        public int SpecializationId { get; set; }

        public Specialization specialization { get; set; }

        [Required(ErrorMessage = "Numer PWZ jest wymagany.")]
        [StringLength(10, ErrorMessage = "Numer PWZ może mieć maksymalnie 10 znaków.")]
        [RegularExpression(@"^\d{7,10}$", ErrorMessage = "Numer PWZ musi zawierać od 7 do 10 cyfr.")]
        public string DoctorNumber { get; set; }

        [Required(ErrorMessage = "Płeć jest wymagana.")]
        public int GenderId { get; set; }

        public Gender gender { get; set; }

        [Required]
        public int UserId { get; set; }

        public User user { get; set; }
    }
}
