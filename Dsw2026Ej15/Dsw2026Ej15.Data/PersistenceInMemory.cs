using Dsw2026Ej15.Domain.Dominio;
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

        public List<Doctor> GetAllActiveDoctors() 
        {
            return _doctors.Where(d  => d.IsActive).ToList();
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

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.FirstOrDefault(s => s.Id == id);
        }
        
        






        






        }



    }

