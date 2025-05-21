using Microsoft.EntityFrameworkCore;
using MediCare.Data.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MediCare.Data
{
    public class DB_MediCareContext : DbContext
    {
        public DB_MediCareContext(DbContextOptions<DB_MediCareContext> options)
    : base(options)
        {
        }

        public DB_MediCareContext()
        : base(new DbContextOptionsBuilder<DB_MediCareContext>()
              .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DB_MediCare;Trusted_Connection=True;")
              .Options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentReport> AppointmentReports { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.user)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.user)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.specialization)
                .WithMany()
                .HasForeignKey(d => d.SpecializationId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

          
            modelBuilder.Entity<AppointmentReport>()
                .HasOne(r => r.Appointment)
                .WithMany()
                .HasForeignKey(r => r.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { Id = 1, Name = "Kardiologia" },
                new Specialization { Id = 2, Name = "Dermatologia" },
                new Specialization { Id = 3, Name = "Neurologia" },
                new Specialization { Id = 4, Name = "Pediatria" },
                new Specialization { Id = 5, Name = "Ortopedia" }
            );

        
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "jkowalski@example.com", PasswordHash = "hashed", Role = UserRole.Doctor },
                new User { Id = 2, Email = "anowak@example.com", PasswordHash = "hashed", Role = UserRole.Doctor },
                new User { Id = 3, Email = "pzielinski@example.com", PasswordHash = "hashed", Role = UserRole.Doctor },
                new User { Id = 4, Email = "kwiśniewska@example.com", PasswordHash = "hashed", Role = UserRole.Doctor },
                new User { Id = 5, Email = "tmazur@example.com", PasswordHash = "hashed", Role = UserRole.Doctor },
                new User { Id = 6, Email = "ejankowska@example.com", PasswordHash = "hashed", Role = UserRole.Doctor },
                new User { Id = 7, Email = "alewandowski@example.com", PasswordHash = "hashed", Role = UserRole.Doctor },
                new User { Id = 8, Email = "mdąbrowska@example.com", PasswordHash = "hashed", Role = UserRole.Doctor },
                new User { Id = 9, Email = "mkaczmarek@example.com", PasswordHash = "hashed", Role = UserRole.Doctor },
                new User { Id = 10, Email = "bszymańska@example.com", PasswordHash = "hashed", Role = UserRole.Doctor }
            );

      
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, FirstName = "Jan", LastName = "Kowalski", PhoneNumber = "123456789", Gender = GenderType.Male, SpecializationId = 1, UserId = 1, DoctorNumber = "1000001" },
                new Doctor { Id = 2, FirstName = "Anna", LastName = "Nowak", PhoneNumber = "987654321", Gender = GenderType.Female, SpecializationId = 2, UserId = 2, DoctorNumber = "1000002" },
                new Doctor { Id = 3, FirstName = "Piotr", LastName = "Zieliński", PhoneNumber = "111222333", Gender = GenderType.Male, SpecializationId = 3, UserId = 3, DoctorNumber = "1000003" },
                new Doctor { Id = 4, FirstName = "Katarzyna", LastName = "Wiśniewska", PhoneNumber = "444555666", Gender = GenderType.Female, SpecializationId = 4, UserId = 4, DoctorNumber = "1000004" },
                new Doctor { Id = 5, FirstName = "Tomasz", LastName = "Mazur", PhoneNumber = "777888999", Gender = GenderType.Male, SpecializationId = 5, UserId = 5, DoctorNumber = "1000005" },
                new Doctor { Id = 6, FirstName = "Ewa", LastName = "Jankowska", PhoneNumber = "222333444", Gender = GenderType.Female, SpecializationId = 1, UserId = 6, DoctorNumber = "1000006" },
                new Doctor { Id = 7, FirstName = "Andrzej", LastName = "Lewandowski", PhoneNumber = "555666777", Gender = GenderType.Male, SpecializationId = 2, UserId = 7, DoctorNumber = "1000007" },
                new Doctor { Id = 8, FirstName = "Magdalena", LastName = "Dąbrowska", PhoneNumber = "888999000", Gender = GenderType.Female, SpecializationId = 3, UserId = 8, DoctorNumber = "1000008" },
                new Doctor { Id = 9, FirstName = "Marek", LastName = "Kaczmarek", PhoneNumber = "101202303", Gender = GenderType.Male, SpecializationId = 4, UserId = 9, DoctorNumber = "1000009" },
                new Doctor { Id = 10, FirstName = "Barbara", LastName = "Szymańska", PhoneNumber = "404505606", Gender = GenderType.Female, SpecializationId = 5, UserId = 10, DoctorNumber = "1000010" }
            );
        }
    }
}
