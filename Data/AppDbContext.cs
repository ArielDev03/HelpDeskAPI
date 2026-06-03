using HelpDeskAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        { 
        }

        public DbSet<User> Users { get; set; }
    }
}
