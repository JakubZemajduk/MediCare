using System.ComponentModel.DataAnnotations;

namespace MediCare.Data.Models
{
    public class AppointmentReport
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Wizyta jest wymagana.")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        [Required(ErrorMessage = "Nazwa choroby jest wymagana.")]
        [MaxLength(100, ErrorMessage = "Choroba może mieć maksymalnie 100 znaków.")]
        public string Disease { get; set; }

        [Required(ErrorMessage = "Opis wizyty jest wymagany.")]
        [MaxLength(1000, ErrorMessage = "Opis może mieć maksymalnie 1000 znaków.")]
        public string Description { get; set; }

        [MaxLength(500, ErrorMessage = "Recepta może mieć maksymalnie 500 znaków.")]
        public string Prescription { get; set; }

        
    }
}
