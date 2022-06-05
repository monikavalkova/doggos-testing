using Doggo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Doggo.Web3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<AFA> AnimalsForAdoption { get; set; }
    }
}
