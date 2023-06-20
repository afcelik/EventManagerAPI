using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.ORM.Models
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceAddress { get; set; }
        public virtual ICollection<Event> Events { get; }

    }
}
