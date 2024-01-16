using BlitzFlug.Models;
using Microsoft.EntityFrameworkCore;

namespace BlitzFlug.Data
{
    public class BlitzFlugContext : DbContext
    {
        public BlitzFlugContext(DbContextOptions<BlitzFlugContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Plane> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
