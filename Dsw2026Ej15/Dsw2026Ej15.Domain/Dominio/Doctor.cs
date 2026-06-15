namespace Dsw2026Ej15.Domain.Dominio
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsActive { get; set; }
        public Speciality Speciality { get; set; }
    }
}
