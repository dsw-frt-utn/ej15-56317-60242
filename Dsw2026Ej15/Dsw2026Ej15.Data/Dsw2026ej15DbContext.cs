using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class Dsw2026ej15DbContext : DbContext
    {
        DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }

        public Dsw2026ej15DbContext(DbContextOptions<Dsw2026ej15DbContext> options) : base(options)
        {
        }
        // Define DbSet properties for your entities
        // public DbSet<Doctor> Doctors { get; set; }
        // public DbSet<Speciality> Specialities { get; set; }
       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure entity mappings here if needed
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the Doctor entity
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctors");
                //entity.Property(d => d.Name).ValueGeneratedOnAdd();
                entity.Property(d => d.Name).HasMaxLength(100);
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired();
                entity.Property(d => d.LicenseNumber).IsRequired();
                //entity.Property(d => d.LicenseNumber).IsUnique();
                entity.Property(d => d.IsActive).IsRequired();
                // Configure the relationship with Speciality
                entity.HasOne(d => d.Speciality)
                      .WithMany()
                      .HasForeignKey(d => d.SpecialityId);
            });
            // Configure the Speciality entity
            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired();
                entity.Property(s => s.Description).IsRequired();
            });
        }
    }
}
