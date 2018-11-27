#region Libraries
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using IDMONEY.IO.Transactions;
using System.Data;
using IDMONEY.IO.Data;
using IDMONEY.IO.Accounts;
using System.Threading.Tasks;
#endregion

namespace IDMONEY.IO.Databases
{
    public class BusinessDatabase : RelationalDatabaseAsync
    {
        public virtual async Task<long> InsertBusinessAsync(Business business)
        {
            MySqlCommand cmd = new MySqlCommand("sp_InsertBusiness", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_name", business.Name);
            cmd.Parameters.AddWithValue("@p_description", business.Description);
            cmd.Parameters.AddWithValue("@p_image", business.Image);
            cmd.Parameters.Add(new MySqlParameter("@p_id", MySqlDbType.Int64));
            cmd.Parameters["@p_id"].Direction = ParameterDirection.Output;

            await cmd.ExecuteNonQueryAsync();

            return Convert.ToInt64(cmd.Parameters["@p_id"].Value);
        }

        public virtual async Task<IList<Business>> SearchBusinessAsync(string name)
        {

            var parameters = DataParameterBuilder.Create(this.GetFactory())
                      .AddInParameter("@piv_name", DbType.String, name)
                      .Parameters;

            IList<Business> list = new List<Business>();
            await this.ExecuteReaderAsync("sp_SearchBusiness", CommandType.StoredProcedure, parameters, (reader) => this.MapEntities(reader, ref list, this.FormatBusiness));
            return list;

        }

        public virtual async Task<Business> GetBusinessAsync(long businessId)
        {
            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                  .AddInParameter("@p_businessId", DbType.Int64, businessId)
                                  .Parameters;

            Business business = default(Business);
            await this.ExecuteReaderAsync("sp_GetBusiness", CommandType.StoredProcedure, parameters, (reader) => this.MapEntity(reader, ref business, this.FormatBusiness));
            return business;
        }

        public virtual async Task<IList<Business>> GetBusinessByIdAsync(long id)
        {
            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                  .AddInParameter("@p_user_id", DbType.Int64, id)
                                  .Parameters;

            IList<Business> list = new List<Business>();
            await this.ExecuteReaderAsync("sp_GetBusinessByUserId", CommandType.StoredProcedure, parameters, (reader) => this.MapEntities(reader, ref list, this.FormatBusiness));
            return list;
        }

        #region Private Methods
        private Business FormatBusiness(IDataReader reader)
        {
            var business = new Business()
            {

                Image = reader["image"].ToString(),
                Description = reader["description"].ToString(),
                Name = reader["name"].ToString(),
                Id = Convert.ToInt32(reader["id"]),
                Account = reader.FormatAccount()
            };

            return business;
        }
        #endregion
    }
}
