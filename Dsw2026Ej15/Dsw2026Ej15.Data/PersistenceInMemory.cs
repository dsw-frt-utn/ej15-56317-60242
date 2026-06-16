using Dsw2026Ej15.Domain;
using System.Numerics;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors;
        private readonly List<Speciality> _specialities;

        public PersistenceInMemory()
        {
            _doctors = new List<Doctor>();
            _specialities = LoadSpecialities();
        }

        private List<Speciality> LoadSpecialities()
        {
        try{
                string json = File.ReadAllText("specialities.json");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<Speciality>>(json, options) ?? new List<Speciality>();

        }
            catch(Exception ex){
                Console.WriteLine($"Error al cargar especialidades: {ex.Message}");
                return new List<Speciality>();
}

        }
        
        public void AddDoctor(Doctor doctor)
        {
            doctor.Id = Guid.NewGuid();
            doctor.IsActive = true;
            _doctors.Add(doctor);
        }


        public void AddSpeciality(Speciality speciality)
        {
            speciality.Id = Guid.NewGuid();
            _specialities.Add(speciality);
        }


        public List<Doctor> GetAllActiveDoctors() 
        {
            return _doctors.Where(d  => d.IsActive).ToList();
        }

        public List<Doctor> GetDoctors()
        {
            return _doctors;
        }

        public Doctor? GetActiveDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        }

        public void DeactivateDoctor(Guid id)
        {
            var doctor = GetActiveDoctorById(id);
            if (doctor != null)
            {
                doctor.IsActive = false;
            }
        }


        public void UpdateDoctor(Doctor doctor)
        {
            var existing = _doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (existing != null)
            {
                existing.Name = doctor.Name;
                existing.LicenseNumber = doctor.LicenseNumber;
                existing.IsActive = doctor.IsActive;
                existing.Speciality = doctor.Speciality;
            }
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.FirstOrDefault(s => s.Id == id);
        }
        
        }

    }

