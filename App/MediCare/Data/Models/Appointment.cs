using System;
using System.ComponentModel.DataAnnotations;

namespace MediCare.Data.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pacjent jest wymagany.")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }


        [Required(ErrorMessage = "Lekarz jest wymagany.")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }


        [Required(ErrorMessage = "Data i godzina wizyty są wymagane.")]
        [DataType(DataType.DateTime, ErrorMessage = "Nieprawidłowy format daty.")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Status wizyty jest wymagany.")]
        public AppointmentStatus Status { get; set; }

    }
}
