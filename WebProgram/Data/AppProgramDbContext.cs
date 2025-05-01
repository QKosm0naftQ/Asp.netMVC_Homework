using Microsoft.EntityFrameworkCore;
using WebProgram.Data.Entities;

namespace WebProgram.Data
{
    public class AppProgramDbContext : DbContext
    {
        public AppProgramDbContext(DbContextOptions<AppProgramDbContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
