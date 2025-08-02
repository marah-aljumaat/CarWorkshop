namespace CarWorkshopAPI.DTO
{
    public class InventoryDto
    {
        public int InventoryId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemType { get; set; }
        public decimal? ItemPrice { get; set; }
        public string? ItemStatus { get; set; }
    }
}
