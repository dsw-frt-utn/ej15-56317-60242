using Microsoft.EntityFrameworkCore;
using System.Linq;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026ej15DbContext _context;

        public PersistenceEf(Dsw2026ej15DbContext context)
        {
            _context = context;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task AddSpecialityAsync(Speciality speciality)
        {
            _context.Specialities.Add(speciality);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateDoctor(Guid id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor != null)
            {
                doctor.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Doctor?> GetActiveDoctorById(Guid id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<List<Doctor>> GetAllActiveDoctorsAsync() //getAllDoctorsAsync
        {
            return await _context.Doctors
                .Where(d => d.IsActive)
                .ToListAsync();
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            return await _context.Specialities.FirstOrDefaultAsync(s => s.Id == id);
        }
        

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);     
            await _context.SaveChangesAsync();
        }
    }
}
