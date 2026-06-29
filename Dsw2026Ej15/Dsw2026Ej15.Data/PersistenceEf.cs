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
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public Task AddSpecialityAsync(Speciality speciality)
        {
            throw new NotImplementedException();
        }

        public void DeactivateDoctor(Guid id)
        {
            throw new NotImplementedException();
        }

        public Doctor? GetActiveDoctorById(Guid id)
        {
            throw new NotImplementedException();
            //return await _context.Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        }

        public async Task<List<Doctor>> GetAllActiveDoctorsAsync() //getAllDoctorsAsync
        {
            return _context.Doctors
                .Where(d => d.IsActive);
        }

        public Task<List<Doctor>> GetDoctorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDoctorAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
