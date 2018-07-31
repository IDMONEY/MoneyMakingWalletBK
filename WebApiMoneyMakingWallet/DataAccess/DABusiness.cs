﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Entities;

namespace IDMONEY.IO.DataAccess
{
    public class DABusiness : DataAccess
    {
        public List<Entities.Business> SearchBusiness(string name)
        {
            MySqlCommand cmd = new MySqlCommand("sp_SearchBusiness", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@piv_name", name);

            List<Entities.Business> list = new List<Entities.Business>();

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Entities.Business()
                    {
                        Image = reader["image"].ToString(),
                        Description = reader["description"].ToString(),
                        Name = reader["name"].ToString(),
                        Id = Convert.ToInt32(reader["id"]),
                    });
                }
            }

            return list;
        }

        public Entities.Business GetBusiness(int businessId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetBusiness", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_businessId", businessId);

            Entities.Business business = null;

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    business = new Entities.Business()
                    {
                        Image = reader["image"].ToString(),
                        Description = reader["description"].ToString(),
                        Name = reader["name"].ToString(),
                        Id = Convert.ToInt32(reader["id"]),
                        AvailableBalance = Convert.ToDecimal(reader["available_balance"]),
                        BlockedBalance = Convert.ToDecimal(reader["blocked_balance"])
                    };
                }
            }

            return business;
        }
    }
}