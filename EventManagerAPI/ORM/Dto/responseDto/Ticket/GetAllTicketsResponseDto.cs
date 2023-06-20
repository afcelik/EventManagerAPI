using EventManagerAPI.ORM.Enums;

namespace EventManagerAPI.ORM.Dto.responseDto.Ticket
{
    public class GetAllTicketsResponseDto
    {
        public int TicketId { get; set; }
        public int CustomerId { get; set; }
        public int EventId { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime TicketPurchaseDate { get; set; }
        public decimal TicketPrice
        {
            get; set;
        }
    }
}
