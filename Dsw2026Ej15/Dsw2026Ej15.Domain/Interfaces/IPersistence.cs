//using Dsw2026Ej15.Data;

using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    List<Doctor> GetDoctors();
    List<Doctor> GetAllActiveDoctors();
    void AddDoctor(Doctor doctor);
    void AddSpeciality(Speciality speciality);
    void DeactivateDoctor(Guid id);
    void UpdateDoctor(Doctor doctor);

    Doctor? GetActiveDoctorById(Guid id);
    Speciality? GetSpecialityById(Guid id);
}
