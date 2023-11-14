using Microsoft.EntityFrameworkCore;

namespace ToolWebAPI.Models
{
    public class ToolContext: DbContext
    {
        public ToolContext(DbContextOptions<ToolContext> options)
            : base(options) { }

        public DbSet<ToolItem> ToolItems { get; set; }
    }
}
