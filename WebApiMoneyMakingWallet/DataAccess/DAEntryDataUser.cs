using IDMONEY.IO.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.DataAccess
{
    public class DAEntryDataUser : DataAccess
    {
        public List<DataEntry> SearchEntryDataByUser(long userId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetEntryDataByUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_user_id", userId);

            List<DataEntry> dataEntries = new List<DataEntry>();

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataEntries.Add(new DataEntry()
                    {
                        Code = reader["code"].ToString(),
                        Description = reader["description"].ToString(),
                        SettingId = Convert.ToInt32(reader["setting_id"]),
                        Type = reader["type"].ToString(),
                        Value = reader["value"].ToString(),
                    });
                }
            }

            return dataEntries;
        }

        public void InsertEntryDataByUser(long userId, List<DataEntry> dataEntries)
        {
            MySqlTransaction tr = null;
            try
            {
                tr = Connection.BeginTransaction();

                MySqlCommand cmd = new MySqlCommand("sp_DeleteEntryDataByUser", Connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_user_id", userId);
                cmd.ExecuteNonQuery();

                foreach (DataEntry dataEntry in dataEntries)
                {
                    cmd = new MySqlCommand("sp_InsertEntryDataByUser", Connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_user_id", userId);
                    cmd.Parameters.AddWithValue("@p_setting_id", dataEntry.SettingId);
                    cmd.Parameters.AddWithValue("@p_value", dataEntry.Value);
                    cmd.ExecuteNonQuery();
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                throw ex;
            }
        }
    }
}
