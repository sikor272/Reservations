using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        // All models of table in database
        public DbSet<Users> Users { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
    }
}
