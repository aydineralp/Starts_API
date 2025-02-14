using APIstarts_API.Models;
using Microsoft.EntityFrameworkCore;

namespace APIstarts_API.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = " Nice villa",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now,

                },
                new Villa()
                {
                    Id = 2,
                    Name = "Pool Villa",
                    Details = " good villa",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg",
                    Occupancy = 8,
                    Rate = 300,
                    Sqft = 650,
                    Amenity = "",
                    CreatedDate = DateTime.Now,

                },
                new Villa()
                {
                    Id = 3,
                    Name = "City Villa",
                    Details = " nicee villa",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg",
                    Occupancy = 8,
                    Rate = 300,
                    Sqft = 650,
                    Amenity = "",
                    CreatedDate = DateTime.Now,

                }
                
            );
        }
    }
}
