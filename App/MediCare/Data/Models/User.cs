using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MediCare.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail.")]
        [MaxLength(100, ErrorMessage = "Adres e-mail może mieć maksymalnie 100 znaków.")]
        public string AddressEmail { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
            ErrorMessage = "Hasło musi zawierać co najmniej jedną małą literę, jedną dużą literę oraz jedną cyfrę.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Rola użytkownika jest wymagana.")]
        public UserRole Role { get; set; }
    }
}

