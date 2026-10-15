using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalSystem.Models
{
    public class RentalCar
    {
        [Key]
        public int RentalCarId { get; set; }

        [ForeignKey(nameof(Rental))]
        public int RentalId { get; set; }

        public Rental? Rental { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public Car? Car { get; set; }

        [Range(1, 1000)]
        [DataType(DataType.Currency)]
        public decimal AgreedDailyPrice { get; set; }
    }
}
