using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.ADO
{
    public class PurchaseRepositoryADO : IPurchaseRepository
    {
        public List<Purchase> GetAllPurchases()
        {
            List<Purchase> purchases = new List<Purchase>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchasesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Purchase currentRow = new Purchase();
                        currentRow.PurchaseId = (int)dr["PurchaseId"];
                        currentRow.PurchaseType.PurchaseTypeId = (int)dr["PurchaseTypeId"];
                        currentRow.Vehicle.VehicleId = dr["VehicleId"].ToString();
                        currentRow.Customer.CustomerId = (int)dr["CustomerId"];
                        currentRow.PurchaseDate = (DateTime)dr["PurchaseDate"];
                        currentRow.PurchasePrice = (decimal)dr["PurchasePrice"];
                        currentRow.UserEmail = dr["UserEmail"].ToString();

                        purchases.Add(currentRow);
                    }
                }
            }
            return purchases;
        }

        public void InsertPurchase(Purchase purchase)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@PurchaseId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@PurchaseTypeId", purchase.PurchaseType.PurchaseTypeId);
                cmd.Parameters.AddWithValue("@VehicleId", purchase.Vehicle.VehicleId);
                cmd.Parameters.AddWithValue("@CustomerId", purchase.Customer.CustomerId);
                cmd.Parameters.AddWithValue("@PurchasePrice", purchase.PurchasePrice);
                cmd.Parameters.AddWithValue("@UserEmail", purchase.UserEmail);

                cn.Open();

                cmd.ExecuteNonQuery();

                purchase.PurchaseId = (int)param.Value;
            }
        }
        
        public List<SalesReport> GetSalesReport(SalesReportSearchParameters parameters)
        {
            List<SalesReport> salesReport = new List<SalesReport>();

            using(var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!String.IsNullOrEmpty(parameters.UserEmail))
                {
                    cmd.Parameters.AddWithValue("@UserEmail", parameters.UserEmail);
                }
                if (parameters.FromDate == DateTime.MinValue)
                {
                    parameters.FromDate = Convert.ToDateTime("1/1/1900");
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate);
                }
                    
                if ((parameters.ToDate == DateTime.MinValue))
                {
                    parameters.ToDate = DateTime.Now;
                    cmd.Parameters.AddWithValue("@ToDate", parameters.ToDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ToDate", parameters.ToDate);
                }

                cn.Open();

                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReport currentRow = new SalesReport();
                        currentRow.User = dr["User"].ToString();
                        currentRow.TotalSales = (decimal)dr["TotalSales"];
                        currentRow.TotalVehicles = (int)dr["TotalVehicles"];

                        salesReport.Add(currentRow);
      
                    }
                }

            }

            return salesReport;
        }
    }
}
