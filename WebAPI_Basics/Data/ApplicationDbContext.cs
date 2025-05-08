using Microsoft.EntityFrameworkCore;
using WebAPI_Basics.Models;

namespace WebAPI_Basics.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }

        public DbSet<Country> countries { get; set; }
    }
}
