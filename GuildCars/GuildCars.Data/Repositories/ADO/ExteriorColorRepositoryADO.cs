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
    public class ExteriorColorRepositoryADO : IExteriorColorRepository
    {
        public List<ExteriorColor> GetExteriorColors()
        {
            List<ExteriorColor> colors = new List<ExteriorColor>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ExteriorColorSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ExteriorColor currentRow = new ExteriorColor();
                        currentRow.ExteriorColorId = (int)dr["ExteriorColorId"];
                        currentRow.ExteriorColorName = dr["ExteriorColorName"].ToString();

                        colors.Add(currentRow);
                    }
                }
            }
            return colors;
        }
    }
}
