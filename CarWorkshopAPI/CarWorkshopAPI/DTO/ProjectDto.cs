namespace CarWorkshopAPI.DTO
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public int? CustomerId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public DateOnly? ProjectStartDate { get; set; }
        public DateOnly? ProjectEndDate { get; set; }
        public string? ProjectStatus { get; set; }
    }
}
