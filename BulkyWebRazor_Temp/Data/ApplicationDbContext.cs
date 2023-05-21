using BulkyWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor_Temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /*
         *  HOW TO CREATE A TABLE IN THE DB with Entity Framework
         *  
         *  1 - Create a Model with some properties
         *  2 - In ApplicationDbContext, create a DbSet for that model
         *  3 - write "add-migration" with a meaningful name (such as AddCategoryTableToDb) on the Package Manager Console (PM console)
         *  4 - write "update-database" on the PM console 
        */


        public DbSet<Category> Categories { get; set; }

        // This ModelBuilder is able to seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }
    }
}
