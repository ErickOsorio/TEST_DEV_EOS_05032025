using Microsoft.EntityFrameworkCore;

namespace ApplicationToka.Data
{
    public class ApplicationTokaContext : DbContext
    {
        public ApplicationTokaContext(DbContextOptions<ApplicationTokaContext> options)
            : base(options)
        {
        }

        public DbSet<Core.Entities.Person> Person { get; set; } = default!;
    }
}
