using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class VehicleModel
    {
        public int ModelId { get; set; }
        public Make Make { get; set; }
        public string ModelName { get; set; }
        public string UserEmail { get; set; }
        public DateTime DateModelCreated { get; set; }

        public VehicleModel()
        {
            Make = new Make();
        }
    }
}
