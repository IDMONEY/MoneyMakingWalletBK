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
        public async Task<long> AddAsync(User user)
        {
            using (var database = new UserDatabase())
            {
                return await database.InsertUserAsync(user);
            }
        }

        public async Task<User> GetByCredentialsAsync(string email, string password)
        {
            using (var database = new UserDatabase())
            {
                return await database.LoginUserAsync(email, password);
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            using (var database = new UserDatabase())
            {
                return await database.GetUserAsync(email);
            }
        }

        public async Task<User> GetByIdAsync(long id)
        {
            using (var database = new UserDatabase())
            {
                return await database.GetUserAsync(id);
            }
        }

        public async Task<User> GetByNicknameAsync(string nickname)
        {
            using (var database = new UserDatabase())
            {
                return await database.GetByNicknameAsync(nickname);
            }
        }
    }
}
