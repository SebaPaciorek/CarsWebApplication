using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarsWebApplication.Model
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Value for {0} must cannot be more than {1}")]
        public string Brand { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Value for {0} must cannot be more than {1}")]
        public string Model { get; set; }
        [StringLength(50, ErrorMessage = "Value for {0} must cannot be more than {1}")]
        public string Color { get; set; }
        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Display(Name = "Fuel Economy")]
        public int FuelEconomy { get; set; }
        [Range(0, 1000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Cost { get; set; }
    }
}
