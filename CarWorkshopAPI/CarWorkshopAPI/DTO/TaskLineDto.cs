namespace CarWorkshopAPI.DTO
{
    public class TaskLineDto
    {
        public int TaskLineId { get; set; }
        public int? TaskId { get; set; }
        public int? InventoryId { get; set; }
        public int? EmployeeId { get; set; }

        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? LineTotal { get; set; }

        public string? TaskLineDescription { get; set; }
    }
}
