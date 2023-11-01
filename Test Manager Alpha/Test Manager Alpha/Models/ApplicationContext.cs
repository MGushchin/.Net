using Microsoft.EntityFrameworkCore;

namespace Test_Manager_Alpha.Models
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options) 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
