﻿using Microsoft.EntityFrameworkCore;
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
                .HasOne(d => d.Specialization)
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

      
        }
    }
}
