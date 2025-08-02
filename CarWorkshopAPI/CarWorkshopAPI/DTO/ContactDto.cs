namespace CarWorkshopAPI.DTO
{
    public class ContactDto
    {
        public int ContactId { get; set; }

        public int? CustomerId { get; set; }

        public string? ContactName { get; set; }

        public string? ContactRole { get; set; }

        public string? ContactPhone { get; set; }

        public string? ContactEmail { get; set; }
    }
}
