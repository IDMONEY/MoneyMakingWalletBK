#region Libraries
using System;
using IDMONEY.IO.Accounts;
using IDMONEY.IO.Users; 
#endregion

namespace IDMONEY.IO.Databases
{
    public class FakeUserDatabase : RelationalDatabase
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
                Account = new Account()
                {
                    Id = Int64.MaxValue,
                    Address = "1x454545aikauajzafq2a6aya1q9a3a",
                    Type = AccountType.Personal,
                    Balance = new Balance()
                    {
                        Available = available,
                        Blocked = random.Next(1000, available),
                    }
                },
                
                Password = "p$23Zsds",
                Privatekey = "AxT1a56"
            };

        }
    }
}