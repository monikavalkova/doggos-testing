using Anima.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Anima.WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<AFA> AnimalsForAdoption { get; set; }
    }
}
