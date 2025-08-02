namespace CarWorkshopAPI.DTO
{
    public class AttendanceDto
    {
        public int EmployeeId { get; set; }
        public string? AttendanceStatus { get; set; }
        public DateOnly? AttendanceDate { get; set; }
    }
}
