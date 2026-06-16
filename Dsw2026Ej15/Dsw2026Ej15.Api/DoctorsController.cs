using Dsw2026Ej15.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api
{
    [ApiController]
    [Route("[api/controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;
        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Name es requerido");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                return BadRequest("LicenseNumber es requerido");

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
                return BadRequest("SpecialityId no existe");

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LicenseNumber = request.LicenseNumber,
                Speciality = speciality,
                IsActive = true
            };

            _persistence.AddDoctor(doctor);

            return StatusCode(201);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _persistence
                .GetDoctors()
                .Where(d => d.IsActive)
                .ToList();

            return Ok(doctors);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var doctor = _persistence.GetActiveDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound();

            var response = new
            {
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name
            };


            return Ok(response);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var doctor = _persistence.GetActiveDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound();

            doctor.IsActive = false;
            _persistence.UpdateDoctor(doctor);

            return NoContent(); // 204
        }

    }
}
