//using Dsw2026Ej15.Data;

using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    public Task<List<Doctor>> GetDoctorsAsync();
    public Task<List<Doctor>> GetAllActiveDoctorsAsync();
    public Task AddDoctorAsync(Doctor doctor);
    public Task AddSpecialityAsync(Speciality speciality);
    public Task DeactivateDoctor(Guid id);
    public Task UpdateDoctorAsync(Doctor doctor);

    public Task<Doctor?> GetActiveDoctorById(Guid id);
    public Task<Speciality?> GetSpecialityByIdAsync(Guid id);
}
