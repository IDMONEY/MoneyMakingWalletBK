#region Libraries
using System.Threading.Tasks;
using IDMONEY.IO.Databases;
using IDMONEY.IO.Users; 
#endregion

namespace IDMONEY.IO.Infrastructure
{
    public class MySqlUserRepository : IUserRepository
    {
        //TODO: How to inject the database
        public async Task<long> Add(User user)
        {
            using (var database = new UserDatabase())
            {
                return await database.InsertUser(user);
            }
        }

        public User GetByCredentials(string email, string password)
        {
            using (var database = new UserDatabase())
            {
                return database.LoginUser(email, password);
            }
        }

        public User GetByEmail(string email)
        {
            using (var database = new UserDatabase())
            {
                return database.GetUser(email);
            }
        }

        public User GetById(long id)
        {
            using (var database = new UserDatabase())
            {
                return database.GetUser(id);
            }
        }
    }
}
