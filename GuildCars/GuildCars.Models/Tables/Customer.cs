using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public State State { get; set; }
        public string CustomerName { get; set; }
        public string  CustomerPhone { get; set; }
        public string CustomerAddress1 { get; set; }
        public string CustomerAddress2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string CustomerEmail { get; set; }

        public Customer()
        {
            State = new State();
        }
    }
}
