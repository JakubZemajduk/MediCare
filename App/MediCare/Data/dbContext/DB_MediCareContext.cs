using Microsoft.EntityFrameworkCore;
using MediCare.Data.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MediCare.Data
{
    public class DB_MediCareContext : DbContext
    {
        public DB_MediCareContext() : base(
       new DbContextOptionsBuilder<DB_MediCareContext>()
           .UseSqlServer(AppConfig.ConnectionString)
           .Options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentReport> AppointmentReports { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Gender> Genders { get; set; }

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

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.gender)
                .WithMany()
                .HasForeignKey(d => d.GenderId);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.gender)
                .WithMany()
                .HasForeignKey(p => p.GenderId);

            
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
        }
    }
}
