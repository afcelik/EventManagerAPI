using EventManagerAPI.ORM.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.ORM.Context
{
    public class EventContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OVRMDFF; Database=EventManagerDb; trusted_connection=true");
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }

}
