using Microsoft.EntityFrameworkCore;

namespace Test_Manager_Alpha.Models
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<TestSuite> TestSuites { get; set; } = null!;
        public DbSet<TestCase> TestCases { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options) 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            string testProject1Name = "Genie2000";
            string testProject2Name = "Apex";

            Project testProject1 = new Project() { Name = testProject1Name, Description = "Test description" };
            Project testProject2 = new Project() { Name = testProject2Name, Description = "Test description" };

            Projects.Add(testProject1);
            Projects.Add(testProject2);

            SaveChanges();
        }

    }
}
