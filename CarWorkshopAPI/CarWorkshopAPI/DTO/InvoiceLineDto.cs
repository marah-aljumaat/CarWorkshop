namespace CarWorkshopAPI.DTO
{
    public class InvoiceLineDto
    {
        public int LineId { get; set; }
        public int? TaskLineId { get; set; }
        public int? InvoiceId { get; set; }
        public int? InventoryId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? LineTotal { get; set; }
        public string? LineDescription { get; set; }
    }
}
