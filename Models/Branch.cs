using System.ComponentModel.DataAnnotations;

namespace VehicleRentalSystem.Models
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string BranchName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Location { get; set; } = string.Empty;

        [Phone]
        public string? ContactNumber { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
