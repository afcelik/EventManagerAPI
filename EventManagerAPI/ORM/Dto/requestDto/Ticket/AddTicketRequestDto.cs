using EventManagerAPI.ORM.Enums;

namespace EventManagerAPI.ORM.Dto.requestDto.Ticket
{
    public class AddTicketRequestDto
    {
        public int CustomerId { get; set; }
        public int EventId { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime TicketPurchaseDate { get; set; }
        public decimal TicketPrice { get; set; }
    }
}
