using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.ORM.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerCity { get; set; }
        public int CustomerAge { get; set; }
        public virtual ICollection<Ticket> Tickets { get; }
    }
}
