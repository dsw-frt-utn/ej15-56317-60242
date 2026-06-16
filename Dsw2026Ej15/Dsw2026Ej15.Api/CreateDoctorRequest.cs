namespace Dsw2026Ej15.Api
{
    public class CreateDoctorRequest
    {
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public Guid SpecialityId { get; set; }
    }
}
