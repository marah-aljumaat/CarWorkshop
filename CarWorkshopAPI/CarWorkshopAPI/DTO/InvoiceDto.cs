namespace CarWorkshopAPI.DTO
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public int? CustomerId { get; set; }
        public DateOnly? DateIssued { get; set; }
        public DateOnly? DueDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? InvoiceNotes { get; set; }
        public string? InvoiceStatus { get; set; }
    }
}
