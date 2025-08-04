namespace CarWorkshopAPI.DTO
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? JobTitle { get; set; }
        public string? Specialty { get; set; }
        public decimal? CommissionRate { get; set; }
        public string? UserName { get; set; }
        //public string? AttendanceStatus { get; set; }
       // public DateOnly? AttendanceDate { get; set; }
    }
}
