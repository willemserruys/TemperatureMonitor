using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TemperatureReading")]
    public class TemperatureReading
    {
        public int ID {get; set;}
        public double Temperature {get; set;}
        public double Humidity {get; set;}
        public DateTime CreatedAt {get; set;}
    }
}