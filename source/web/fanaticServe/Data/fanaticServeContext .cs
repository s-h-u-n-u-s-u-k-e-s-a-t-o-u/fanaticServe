using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Data
{
    public class fanaticServeContext: DbContext
    {
        public fanaticServeContext(DbContextOptions<fanaticServeContext> options) : base(options)
        {
        }

        public DbSet<fanaticServe.Models.Abustract_Album> Abustract_Album { get; set; }
    }
}
