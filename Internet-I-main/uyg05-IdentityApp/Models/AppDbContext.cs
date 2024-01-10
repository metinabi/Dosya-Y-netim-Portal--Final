using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace uyg05_IdentityApp.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<FileManage> FileManagers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
