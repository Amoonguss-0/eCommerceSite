using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class VideoGameContext : DbContext
    {
        public VideoGameContext(DbContextOptions<VideoGameContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Member> Members { get; set; }  
    }
}
