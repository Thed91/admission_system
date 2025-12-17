using admission_system.Server.Enteties;
using Microsoft.EntityFrameworkCore;

namespace admission_system.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<VisitorRequest> Requests { get; set; }
    }
}
