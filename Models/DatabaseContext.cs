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

        public DbSet<Elevators> Elevators { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Batteries>()
                .ToTable("batteries");

             modelBuilder.Entity<Elevators>()
                .ToTable("elevators");

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
