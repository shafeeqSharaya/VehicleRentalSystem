using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalSystem.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Model { get; set; } = string.Empty;

        [Range(2000, 2035)]
        public int ManufactureYear { get; set; }

        [Range(10, 1000)]
        [DataType(DataType.Currency)]
        public decimal DailyPrice { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string PlateNumber { get; set; } = string.Empty;

        [ForeignKey(nameof(Branch))]
        public int BranchId { get; set; }

        public Branch? Branch { get; set; }

        [ForeignKey(nameof(CarCategory))]
        public int CarCategoryId { get; set; }

        public CarCategory? CarCategory { get; set; }

        public ICollection<RentalCar> RentalCars { get; set; } = new List<RentalCar>();
    }
}
