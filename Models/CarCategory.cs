using System.ComponentModel.DataAnnotations;

namespace VehicleRentalSystem.Models
{
    public class CarCategory
    {
        [Key]
        public int CarCategoryId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string CategoryName { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Description { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
