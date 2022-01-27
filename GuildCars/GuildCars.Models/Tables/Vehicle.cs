using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Vehicle
    {
        public string VehicleId { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public InteriorColor InteriorColor { get; set; }
        public ExteriorColor ExteriorColor { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public BodyStyle BodyStyle { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal ListedPrice { get; set; }
        public decimal MSRP { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsUsed { get; set; }
        public bool IsSold { get; set; }
        public string UserEmail { get; set; }

        public Vehicle()
        {
            VehicleModel = new VehicleModel();
            InteriorColor = new InteriorColor();
            ExteriorColor = new ExteriorColor();
            TransmissionType = new TransmissionType();
            BodyStyle = new BodyStyle();
        }
    }
}
