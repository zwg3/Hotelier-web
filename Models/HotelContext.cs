using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;


namespace Hotelier_web.Models
{

    public class HotelContext : DbContext    {
        public HotelContext(DbContextOptions<HotelContext> options)
        : base(options){ }
        public DbSet<User> Users { get; set; }
    }
}
