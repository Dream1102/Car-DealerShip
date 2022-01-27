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
    public class TransmissionTypeRepositoryADO : ITransmissionTypeRepository
    {
        public List<TransmissionType> GetTransmissionTypes()
        {
            List<TransmissionType> transmissionTypes = new List<TransmissionType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TransmissionTypeSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        TransmissionType currentRow = new TransmissionType();
                        currentRow.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        currentRow.TransmissionTypeName = dr["TransmissionTypeName"].ToString();

                        transmissionTypes.Add(currentRow);
                    }
                }
            }
            return transmissionTypes;
        }
    }
}
