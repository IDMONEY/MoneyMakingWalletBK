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
            using (var daUser = new DAUser())
            {
                return daUser.InsertUser(user);
            }
        }

        public User GetByCredentials(string email, string password)
        {
            using (var daUser = new DAUser())
            {
                return daUser.LoginUser(email, password);
            }
        }

        public User GetByEmail(string email)
        {
            using (var daUser = new DAUser())
            {
                return daUser.GetUser(email);
            }
        }

        public User GetById(long id)
        {
            using (var daUser = new DAUser())
            {
                return daUser.GetUser(id);
            }
        }
    }
}
