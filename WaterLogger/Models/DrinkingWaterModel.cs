using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WaterLogger.Models
{
    public class DrinkingWaterModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
    }
}
