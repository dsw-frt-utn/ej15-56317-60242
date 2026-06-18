using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System.Numerics;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors = [];
        private readonly List<Speciality> _specialities = [];

        public PersistenceInMemory()
        {
            //_doctors = new List<Doctor>();
            //_specialities = LoadSpecialities();
            LoadSpecialities();
        }

        private List<Speciality> LoadSpecialities()
        {
        try{
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources","specialities.json");
                string json = File.ReadAllText(jsonPath);

                 var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                  return JsonSerializer.Deserialize<List<Speciality>>(json, options) ?? new List<Speciality>();

                /* var specialities =  JsonSerializer.Deserialize<List<Speciality>>(json,new JsonSerializerOptions() { 
                    PropertyNameCaseInsensitive = true 
                }) ?? [];

                _specialities = [.. specialities.Select(s=> new Speciality(s.Name, s.Description, s.Id))];// convierte En los objetos del dominio especiality*/
        }
            catch(Exception ex){
                Console.WriteLine($"Error al cargar especialidades: {ex.Message}");
                return new List<Speciality>();
}

        }
        
        public Task AddDoctorAsync(Doctor doctor)
        {
            doctor.Id = Guid.NewGuid();
            doctor.IsActive = Task.FromResult(true);
            _doctors.Add(doctor);
            return Task.CompletedTask;
        }


        public Task AddSpecialityAsync(Speciality speciality)
        {
            speciality.Id = Guid.NewGuid();
            _specialities.Add(speciality);
            return Task.CompletedTask;
        }


        public Task<List<Doctor>> GetAllActiveDoctorsAsync()
        {
            return Task.FromResult(_doctors.Where(d => d.IsActive.Result).ToList());
        }

        public Task<List<Doctor>> GetDoctorsAsync()
        {
            return Task.FromResult(_doctors);
        }

        public Task<Doctor?> GetActiveDoctorByIdAsync(Guid id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == id && d.IsActive.Result);
            return Task.FromResult(doctor);
        }

        public Task DeactivateDoctorAsync(Guid id)
        {
            var doctor = GetActiveDoctorByIdAsync(id);
            if (doctor != null)
            {
                doctor.IsActive = Task.FromResult(false);
            }
            return Task.CompletedTask;
        }


        public Task UpdateDoctorAsync(Doctor doctor)
        {
            var existing = _doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (existing != null)
            {
                existing.Name = doctor.Name;
                existing.LicenseNumber = doctor.LicenseNumber;
                existing.IsActive = doctor.IsActive;
                existing.Speciality = doctor.Speciality;
            }
            return Task.CompletedTask;
        }

        public Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            var speciality = _specialities.SingleOrDefault(s => s.Id == id);
            return Task.FromResult(speciality);
        }
        
        }

    }

