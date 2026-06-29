namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsActive { get; set; }
        public Guid? SpecialityId { get; set; }
        public Speciality? Speciality { get; private set; }

        private Doctor() { } // For EF Core

        public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) : base(id)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            IsActive = true;
            Speciality = speciality;
        }
    }
}
