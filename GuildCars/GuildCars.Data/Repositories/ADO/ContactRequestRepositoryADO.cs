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
    public class ContactRequestRepositoryADO : IContactRequestRepository
    {
        public List<ContactRequest> GetAllContactRequests()
        {
            List<ContactRequest> contactRequests = new List<ContactRequest>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactRequestsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ContactRequest currentRow = new ContactRequest();
                        currentRow.ContactRequestId = (int)dr["ContactRequestId"];
                        currentRow.VehicleId = dr["VehicleId"].ToString();
                        currentRow.ContactName = dr["ContactName"].ToString();
                        currentRow.ContactEmail = dr["ContactEmail"].ToString();
                        currentRow.ContactPhone = dr["ContactPhone"].ToString();
                        currentRow.ContactMessage = dr["ContactMessage"].ToString();
                        currentRow.DateContactRequestCreated = (DateTime)dr["DateContactRequestCreated"];

                        contactRequests.Add(currentRow);
                    }
                }
            }
            return contactRequests;
        }

        public void InsertContactRequest(ContactRequest contactRequest)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactRequestInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ContactRequestId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@VehicleId", contactRequest.VehicleId);
                cmd.Parameters.AddWithValue("@ContactName", contactRequest.ContactName);
                cmd.Parameters.AddWithValue("@ContactEmail", contactRequest.ContactEmail);
                cmd.Parameters.AddWithValue("@ContactPhone", contactRequest.ContactPhone);
                cmd.Parameters.AddWithValue("@ContactMessage", contactRequest.ContactMessage);

                cn.Open();

                cmd.ExecuteNonQuery();

                contactRequest.ContactRequestId = (int)param.Value;
            }
        }
    }
}
