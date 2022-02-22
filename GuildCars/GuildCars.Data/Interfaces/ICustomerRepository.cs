using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ICustomerRepository
    {
        void InsertCustomer(Customer customer);
        List<Customer> GetAllCustomers();
    }
}
