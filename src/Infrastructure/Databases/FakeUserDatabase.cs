using System;
using System.Collections.Generic;
using System.Text;
using IDMONEY.IO.Users;

namespace IDMONEY.IO.DataAccess
{
    public class FakeUserDatabase : DataAccess
    {
        Random random = new Random();

        public long InsertUser(User user)
        {
            return Int64.MaxValue;
        }
        public User GetUser(long userId)
        {
            return GetUser();
        }

        public User LoginUser(string email, string password)
        {
            return GetUser();
        }

        public User GetUser(string email)
        {
            return GetUser();
        }


        private User GetUser()
        {
            var available = random.Next(1000, 50000);
            return new User()
            {
                Id = Int64.MaxValue,
                Email = "test@test.com",
                Address = "1x454545aikauajzafq2a6aya1q9a3a",
                AvailableBalance = available,
                BlockedBalance = random.Next(available, 50000),
                Password = "p$23Zsds",
                Privatekey = "AxT1a56"
            };

        }
    }
}