using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.ADO
{
    public class CustomerRepositoryADO : ICustomerRepository
    {
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CustomersSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Customer currentRow = new Customer();
                        currentRow.CustomerId = (int)dr["CustomerId"];
                        currentRow.State.StateAbbreviation = dr["StateAbbreviation"].ToString();
                        currentRow.CustomerName = dr["CustomerName"].ToString();
                        currentRow.CustomerPhone = dr["CustomerPhone"].ToString();
                        currentRow.CustomerAddress1 = dr["CustomerAddress1"].ToString();
                        currentRow.CustomerAddress2 = dr["CustomerAddress2"].ToString();
                        currentRow.City = dr["City"].ToString();
                        currentRow.Zip = dr["Zip"].ToString();
                        currentRow.CustomerEmail = dr["CustomerEmail"].ToString();
                       
                        customers.Add(currentRow);
                    }
                }
            }
            return customers;
        }

        public void Insert(Customer customer)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CustomerInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@CustomerId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@StateAbbreviation", customer.State.StateAbbreviation);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@CustomerPhone", customer.CustomerPhone);
                cmd.Parameters.AddWithValue("@CustomerAddress1", customer.CustomerAddress1);
                cmd.Parameters.AddWithValue("@CustomerAddress2", customer.CustomerAddress2);
                cmd.Parameters.AddWithValue("@City", customer.City);
                cmd.Parameters.AddWithValue("@Zip", customer.Zip);
                cmd.Parameters.AddWithValue("@CustomerEmail", customer.CustomerEmail);

                cn.Open();

                cmd.ExecuteNonQuery();

                customer.CustomerId = (int)param.Value;
            }
        }
    }
}
