using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WaterLogger.Models
{
    public class DrinkingWaterModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Quantity { get; set; }
    }
}
