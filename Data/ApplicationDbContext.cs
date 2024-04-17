using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {


        public DbSet<UserInfo> Users { get; set; }
        public DbSet<WebApplication1.Models.UserLogin> UserLogin { get; set; } = default!;
        public DbSet<WebApplication1.Models.UrlProfile> UrlProfile { get; set; } = default!;
    }
}