﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IDMONEY.IO.Entities;
using MySql.Data.MySqlClient;

namespace IDMONEY.IO.DataAccess
{
    public class DAUser : DataAccess
    {
        public long InsertUser(User user)
        {
            MySqlCommand cmd = new MySqlCommand("sp_InsertUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_email",user.Email);
            cmd.Parameters.AddWithValue("@p_password", user.Password);
            cmd.Parameters.AddWithValue("@p_address", user.Address);
            cmd.Parameters.AddWithValue("@p_private_key", user.Privatekey);
            cmd.Parameters.Add(new MySqlParameter("@p_id", MySqlDbType.Int32));
            cmd.Parameters["@p_id"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            return Convert.ToInt64(cmd.Parameters["@p_id"].Value);
        }

        public User GetUser(long userId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_user_id", userId);

            User user = null;

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new User()
                    {
                        Address = reader["address"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        Privatekey = reader["private_key"].ToString(),
                        Id = Convert.ToInt64(reader["user_id"]),
                    };
                }
            }

            return user;
        }

        public User LoginUser(string email, string password)
        {
            MySqlCommand cmd = new MySqlCommand("sp_LoginUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_email", email);
            cmd.Parameters.AddWithValue("@p_password", password);

            User user = null;

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new User()
                    {
                        Address = reader["address"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        Privatekey = reader["private_key"].ToString(),
                        Id = Convert.ToInt64(reader["user_id"]),
                    };
                }
            }
        
            return user;
        }

        public User GetUser(string email)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetUser", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_email", email);

            User user = null;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new User()
                    {
                        Address = reader["address"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        Privatekey = reader["private_key"].ToString(),
                        Id = Convert.ToInt64(reader["user_id"]),
                    };
                }
            }


            return user;
        }
    }
}
