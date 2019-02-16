using System.ComponentModel.DataAnnotations;

namespace HourRegistration.DataAccess.Models
{
    public class TemperatureReading
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public string TimeStamp { get; set; }
    }
}
