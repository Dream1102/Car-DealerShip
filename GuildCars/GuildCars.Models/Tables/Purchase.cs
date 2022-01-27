using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public PurchaseType PurchaseType { get; set; }
        public Vehicle Vehicle { get; set; }
        public Customer Customer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public string UserEmail { get; set; }

        public Purchase()
        {
            PurchaseType = new PurchaseType();
            Vehicle = new Vehicle();
            Customer = new Customer();
        }
    }
}
