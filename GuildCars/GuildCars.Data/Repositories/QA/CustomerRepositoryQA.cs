using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class CustomerRepositoryQA : ICustomerRepository
    {
        private List<Customer> _customers = new List<Customer>();

        public CustomerRepositoryQA()
        {
            SeedCustomerRepository();
        }

        private void SeedCustomerRepository()
        {
            Customer customer = new Customer();
            customer.CustomerId = 1;
            customer.State.StateAbbreviation = "KY";
            customer.State.StateName = "Kentucky";
            customer.CustomerName = "Telemachus";
            customer.CustomerPhone = "502-555-5555";
            customer.CustomerAddress1 = "523 Colonel Hill";
            customer.City = "Louisville";
            customer.Zip = "40242";
            customer.CustomerEmail = "tel@machus.com";

            _customers.Add(customer);
        }

        public void Insert(Customer customer)
        {
            _customers.Add(customer);
        }

        public List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }
    }
}
