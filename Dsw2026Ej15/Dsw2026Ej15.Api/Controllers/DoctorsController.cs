using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;
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
        public async Task<IActionResult> Create([FromBody] DoctorModel.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Nombre es requerido"); //es un regla de negocio

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                throw new ValidationException("Matricula es requerido"); //return BadRequest("Matricula es requerido")//("Numero de licencia es requerido");

            var speciality = await _persistence.GetSpecialityByIdAsync(request.SpecialityId);
            if (speciality is null)
                throw new ValidationException("Especialidad no existe");

            var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
             
            await _persistence.AddDoctorAsync(doctor);

            return Created();// StatusCode(201);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _persistence.GetAllActiveDoctorsAsync();

            return Ok(doctors.Select(d => new DoctorModel.Response(d.Name,
                d.LicenseNumber, d.Speciality?.Id)));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var doctor =  _persistence.GetActiveDoctorById(id); //await _persistence.GetDoctorByIdAsync(id);

            if (doctor is null || !doctor.IsActive)
                return NotFound("Medico no encontrado");

            var response = new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, doctor.Speciality?.Id);
            
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var doctor =  _persistence.GetActiveDoctorById(id); //await _persistence.GetDoctorByIdAsync(id);

            if (doctor is null || !doctor.IsActive)
                return NotFound("Medico no encontrado");

            _persistence.DeactivateDoctor(id);

            return NoContent(); // 204
        }

    }
}
