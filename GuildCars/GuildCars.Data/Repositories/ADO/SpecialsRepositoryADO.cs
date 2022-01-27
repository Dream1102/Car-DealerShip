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
    public class SpecialsRepositoryADO : ISpecialsRepository
    {
        public List<Special> GetAll()
        {
            List<Special> specials = new List<Special>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Special currentRow = new Special();
                        currentRow.SpecialId = (int)dr["SpecialId"];
                        currentRow.SpecialName = dr["SpecialName"].ToString();
                        currentRow.SpecialImageFilename = dr["SpecialImageFilename"].ToString();
                        currentRow.SpecialDescription = dr["SpecialDescription"].ToString();
                        currentRow.UserEmail = dr["UserEmail"].ToString();

                        specials.Add(currentRow);
                    }
                }
            }
            return specials;
        }

        public void Insert(Special specialItem)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@SpecialId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@SpecialImageFilename", specialItem.SpecialImageFilename);
                cmd.Parameters.AddWithValue("@SpecialName", specialItem.SpecialName);
                cmd.Parameters.AddWithValue("@SpecialDescription", specialItem.SpecialDescription);
                cmd.Parameters.AddWithValue("@UserEmail", specialItem.UserEmail);

                cn.Open();

                cmd.ExecuteNonQuery();

                specialItem.SpecialId = (int)param.Value;
            }
        }

        public void Delete(int specialId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SpecialId", specialId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
