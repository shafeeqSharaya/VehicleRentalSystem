using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<CarCategory> CarCategories { get; set; }

        public DbSet<RentalCar> RentalCars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    BranchId = 1,
                    BranchName = "Amman Branch",
                    Location = "Amman",
                    ContactNumber = "0799999999"
                }
            );

            modelBuilder.Entity<CarCategory>().HasData(
                new CarCategory
                {
                    CarCategoryId = 1,
                    CategoryName = "CLS",
                    Description = "CLS Cars"
                }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    CarId = 1,
                    Brand = "Toyota",
                    Model = "Camry",
                    ManufactureYear = 2022,
                    DailyPrice = 50,
                    PlateNumber = "12-345",
                    BranchId = 1,
                    CarCategoryId = 1
                }
            );
        }
    }
}