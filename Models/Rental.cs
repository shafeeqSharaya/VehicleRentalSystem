using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VehicleRentalSystem.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Currency)]
        [Range(1, 10000)]
        public decimal TotalCost { get; set; }

        public ERentalStatus Status { get; set; } = ERentalStatus.Pending;

        [MaxLength(250)]
        public string? Notes { get; set; }

        public ICollection<RentalCar> RentalCars { get; set; } = new List<RentalCar>();
    }
}
