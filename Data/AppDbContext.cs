using Microsoft.EntityFrameworkCore;
using NewsApplication.Models;

namespace NewsApplication.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQL Server connection string
            optionsBuilder.UseSqlServer("Server=tcp:newsservers.database.windows.net,1433;Initial Catalog=NewsAppDb;Persist Security Info=False;User ID=vijay;Password=Vij@y9705637553;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            
        }
        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<User>().HasData(
        //        new User {Id =1, Username = "Vijay", Email = "VijayKumrdevi9@gamil.com", Password = "12345" },
        //        new User { Id = 2, Username = "ajay", Email = "ajayKumrdevi9@gamil.com", Password = "123456" },
        //        new User { Id = 3, Username = "paul", Email = "paul9@gamil.com", Password = "1234567" },
        //        new User { Id = 4, Username = "max", Email = "max@gamil.com", Password = "12345678" },
        //        new User { Id = 5, Username = "victor", Email = "victor@gamil.com", Password = "123456789" },
        //        new User { Id = 6, Username = "jhon", Email = "jhon@gamil.com", Password = "12345611" }


        //        );

           
        //}
    }
}
