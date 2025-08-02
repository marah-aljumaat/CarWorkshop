namespace CarWorkshopAPI.DTO
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public int? CustomerId { get; set; }
        public int? ProjectId { get; set; }
        public int? CarId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskStatus { get; set; }
        public DateOnly? TaskStartDate { get; set; }
        public DateOnly? TaskEndDate { get; set; }
        public DateOnly? CarReceivedAt { get; set; }
        public DateOnly? CarDeliveredAt { get; set; }
    }
}
