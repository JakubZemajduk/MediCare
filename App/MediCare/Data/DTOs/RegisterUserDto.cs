using System.ComponentModel.DataAnnotations;

namespace MediCare.Data.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail.")]
        [MaxLength(100, ErrorMessage = "Adres e-mail może mieć maksymalnie 100 znaków.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
            ErrorMessage = "Hasło musi zawierać co najmniej jedną małą literę, jedną dużą literę oraz jedną cyfrę.")]
        public string Password { get; set; } = string.Empty;
    }
}
