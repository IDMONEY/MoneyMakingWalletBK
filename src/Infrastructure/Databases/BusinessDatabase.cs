#region Libraries
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using IDMONEY.IO.Transactions;
using System.Data;
using IDMONEY.IO.Data;
using IDMONEY.IO.Accounts;
#endregion

namespace IDMONEY.IO.Databases
{
    public class BusinessDatabase : RelationalDatabase
    {
        public long InsertBusiness(Business business)
        {
            MySqlCommand cmd = new MySqlCommand("sp_InsertBusiness", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_name", business.Name);
            cmd.Parameters.AddWithValue("@p_description", business.Description);
            cmd.Parameters.AddWithValue("@p_image", business.Image);
            cmd.Parameters.Add(new MySqlParameter("@p_id", MySqlDbType.Int64));
            cmd.Parameters["@p_id"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            return Convert.ToInt64(cmd.Parameters["@p_id"].Value);
        }

        public IList<Business> SearchBusiness(string name)
        {

            var parameters = DataParameterBuilder.Create(this.GetFactory())
                      .AddInParameter("@piv_name", DbType.String, name)
                      .Parameters;

            IList<Business> list = new List<Business>();
            this.ExecuteReader("sp_SearchBusiness", CommandType.StoredProcedure, parameters, (reader) => this.MapEntities(reader, ref list, this.FormatBusiness));
            return list;

        }

        public Business GetBusiness(int businessId)
        {
          

            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                  .AddInParameter("@p_businessId", DbType.Int64, businessId)
                                  .Parameters;

            Business business = null;
            this.ExecuteReader("sp_GetBusiness", CommandType.StoredProcedure, parameters, (reader) => this.MapEntity(reader, ref business, this.FormatBusiness));
            return business;
        }

        public virtual IList<Business> GetBusinessById(long id)
        {
            var parameters = DataParameterBuilder.Create(this.GetFactory())
                                  .AddInParameter("@p_user_id", DbType.Int64, id)
                                  .Parameters;

            IList<Business> list = new List<Business>();
            this.ExecuteReader("sp_GetBusinessByUserId", CommandType.StoredProcedure, parameters, (reader) => this.MapEntities(reader, ref list, this.FormatBusiness));
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
                Account = this.FormatAccount(reader)
            };

            return business;
        }

        //TODO: Move this to a single file or use automapper
        private Account FormatAccount(IDataReader reader)
        {
            return new Account()
            {
                Id = Convert.ToInt32(reader["account_id"]),
                Type = AccountType.Business,
                Address = reader["address"].ToString(),
                Balance = this.FormatBalance(reader)
            };
        }

        private Balance FormatBalance(IDataReader reader)
        {
            return new Balance()
            {
                Available = Convert.ToDecimal(reader["available_balance"]),
                Blocked = Convert.ToDecimal(reader["blocked_balance"]),
            };

        }
        #endregion
    }
}
