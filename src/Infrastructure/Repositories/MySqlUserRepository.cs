#region Libraries
using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Users; 
#endregion

namespace IDMONEY.IO.Infrastructure
{
    public class MySqlUserRepository : IUserRepository
    {
        //TODO: How to inject the database
        public long Add(User user)
        {
            using (var database = new DAUser())
            {
                return database.InsertUser(user);
            }
        }

        public User GetByCredentials(string email, string password)
        {
            using (var database = new DAUser())
            {
                return database.LoginUser(email, password);
            }
        }

        public User GetByEmail(string email)
        {
            using (var database = new DAUser())
            {
                return database.GetUser(email);
            }
        }

        public User GetById(long id)
        {
            using (var database = new DAUser())
            {
                return database.GetUser(id);
            }
        }
    }
}
