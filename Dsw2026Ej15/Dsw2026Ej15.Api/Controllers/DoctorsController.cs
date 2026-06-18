using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]// [Route("api")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;
        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]//[HttpPost("doctors")]
        public async Task<IActionResult> Create([FromBody] CreateDoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Nombre es requerido"); //es un regla de negocio

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                return BadRequest("Matricula es requerido");//throw new ValidationException("Numero de licencia es requerido");

            var speciality = await _persistence.GetSpecialityByIdAsync(request.SpecialityId);
            if (speciality == null)
                throw new ValidationException("Especialidad no existe");

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LicenseNumber = request.LicenseNumber,
                Speciality = speciality,
                IsActive = true
            };

            await _persistence.AddDoctorAsync(doctor);

            return Created();// StatusCode(201);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _persistence.GetAllActiveDoctorsAsync();

            return Ok(doctors);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var doctor = await _persistence.GetActiveDoctorByIdAsync(id);

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
        public async Task<IActionResult> Delete(Guid id)
        {
            var doctor = await _persistence.GetActiveDoctorByIdAsync(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound();

            await _persistence.DeactivateDoctorAsync(id);

            return NoContent(); // 204
        }

    }
}
