using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.ADO.VehicleTables
{
    public class ModelRepositoryADO : IModelRepository
    {
        public List<VehicleModel> GetAllVehicleModels()
        {
            List<VehicleModel> models = new List<VehicleModel>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleModel currentRow = new VehicleModel();
                        currentRow.ModelId = (int)dr["ModelId"];
                        currentRow.Make.MakeId = (int)dr["MakeId"];
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.UserEmail = dr["UserEmail"].ToString();
                        currentRow.DateModelCreated = (DateTime)dr["DateModelCreated"];
                        models.Add(currentRow);
                    }
                }
            }
            return models;
        }

        public void InsertVehicleModel(VehicleModel model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeId", model.Make.MakeId);
                cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                cmd.Parameters.AddWithValue("@UserEmail", model.UserEmail);

                cn.Open();

                cmd.ExecuteNonQuery();

                model.ModelId = (int)param.Value;
            }
        }
    }
}
