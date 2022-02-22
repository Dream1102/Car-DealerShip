using GuildCars.Data.Interfaces.VehicleTables;
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
    public class MakeRepositoryADO : IMakeRepository
    {
        public List<Make> GetAllVehicleMakes()
        {
            List<Make> makes = new List<Make>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Make currentRow = new Make();
                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.UserEmail = dr["UserEmail"].ToString();
                        currentRow.DateMakeCreated = (DateTime)dr["DateMakeCreated"];
                        makes.Add(currentRow);
                    }
                }
            }
            return makes;
        }

        public void InsertVehicleMake(Make make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@MakeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeName", make.MakeName);
                cmd.Parameters.AddWithValue("@UserEmail", make.UserEmail);

                cn.Open();

                cmd.ExecuteNonQuery();

                make.MakeId = (int)param.Value;
            }
        }
    }
}
