using Microsoft.EntityFrameworkCore;

namespace RestApi.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Batteries> Batteries { get; set; } // specifies all informations about DB multiple tables


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Batteries>()
                .ToTable("batteries");
        }

    }
}



// public class YourContext : DbContext
// {
//     protected override void OnModelCreating(ModelBuilder builder)
//     {
//         builder.Entity<MyModel>(entity => {
//             entity.ToTable("MyModelTable");
//         });
//     }
// }
