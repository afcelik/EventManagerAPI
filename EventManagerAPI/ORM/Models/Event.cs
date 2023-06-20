using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagerAPI.ORM.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventPerson { get; set; }
        public string EventCategory { get; set; }
        public string EventDescription { get; set; }
        [ForeignKey("PlaceId")]
        public int PlaceId { get; set; }
        public virtual Place Places { get; set; }
        public DateTime EventStartingDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public bool IsEventPaid { get; set; }
        public decimal? EventPrice { get; set; }
        public byte[]? EventImage1 { get; set; }
        public byte[]? EventImage2 { get; set; }
        public byte[]? EventImage3 { get; set; }

    }
}
