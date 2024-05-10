namespace ERPAPI.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNo { get; set; }
        public string? Education { get; set; }
        public string? Designation { get; set; }
        public string? Email { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Area { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? StateRegion { get; set; }
        public string? Postcode { get; set; }
        public bool IsActived { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string GetFullName()
        {
            if (!string.IsNullOrEmpty(MiddleName))
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
            else
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
