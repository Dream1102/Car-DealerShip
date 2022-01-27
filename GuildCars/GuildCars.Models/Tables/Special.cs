using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Special
    {
        public int SpecialId { get; set; }
        public string SpecialImageFilename { get; set; }
        public string SpecialName { get; set; }
        public string SpecialDescription { get; set; }
        public DateTime DateSpecialCreated { get; set; }
        public string UserEmail { get; set; }

    }
}
