using EventManagerAPI.ORM.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace EventManagerAPI.ORM.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public virtual Customer Customers { get; set; }
        [ForeignKey("EventId")]
        public int EventId { get; set; }
        public virtual Event Events { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime TicketPurchaseDate { get; set; }
        public decimal TicketPrice { get; set; }

    }
}
