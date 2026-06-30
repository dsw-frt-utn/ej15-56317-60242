using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {


        private readonly Dsw2026ej15DbContext _context;

        public PersistenceEf(Dsw2026ej15DbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _context.Doctors.Include(d => d.Speciality).ToListAsync();
        }

        public async Task<List<Doctor>> GetAllActiveDoctorsAsync()
        {
            return await _context.Doctors.Include(d => d.Speciality).Where(d => d.IsActive).ToListAsync();
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task AddSpecialityAsync(Speciality speciality)
        {
            await _context.Specialities.AddAsync(speciality);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateDoctor(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                doctor.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<Doctor?> GetActiveDoctorById(Guid id)
        {
            return await _context.Doctors.Include(d => d.Speciality).FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            return await _context.Specialities.FindAsync(id);
        }


    }
}


