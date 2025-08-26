using Brony.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brony.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("");
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Stadium> Stadiums { get; set; }
    public DbSet<Booking> Bookings { get; set; }
}