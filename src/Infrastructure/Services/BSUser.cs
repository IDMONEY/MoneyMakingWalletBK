using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IDMONEY.IO.Services
{
    public class BSUser : BaseBS
    {
        public BSUser(ClaimsPrincipal claimsPrincipal) 
            : base(claimsPrincipal)
        {
        }

        public BSUser() : base()
        {
                
        }

        public CreateUserResponse CreateUser(CreateUserRequest req)
        {
            CreateUserResponse res = new CreateUserResponse();
            try
            {
                using (DAUser daUser = new DAUser())
                {
                    User user = daUser.GetUser(req.Email);

                    if(user == null)
                    {
                        //var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
                        //var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
                        //var account = new Nethereum.Web3.Accounts.Account(privateKey);

                        user = new User()
                        {
                            //Address = account.Address,
                            Email = req.Email,
                            Password = $"{req.Email}:{req.Password}".GenerateSHA512(),
                            //Privatekey = privateKey
                        };

                        user.AvailableBalance = 0;
                        user.BlockedBalance = 0;
                        user.Id = daUser.InsertUser(user);

                        res.User = user;
                        //res.Token = BuildToken(user);
                        res.IsSuccessful = true;
                    }
                    else
                    {
                        res.IsSuccessful = false;
                        res.Errors.Add(new Error() { Code = ((int)ErrorCodes.EmailIsRegistred).ToString(), Message = "That email is taken. Try another" });
                    }
                }
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;

                if (ex.Message.Contains("UK_users_emai"))
                {
                    res.Errors.Add(new Error() { Code = ((int)ErrorCodes.EmailIsRegistred).ToString(), Message = "That email is taken. Try another" });
                }
                else
                {
                    res.Errors.Add(new Error() { Code = ((int)ErrorCodes.ErrorNotSpecific).ToString(), Message = "There was a problem. Please try again later" });
                }
            }
            return res;
        }

        public UserResponse GetUser(Request req)
        {
            UserResponse res = new UserResponse();
            using (DAUser daUser = new DAUser())
            {
                res.User = daUser.GetUser(User.Id);
            }
            res.IsSuccessful = true;
            return res;
        }

        public LoginUserResponse Login(LoginUserRequest req)
        {
            LoginUserResponse res = new LoginUserResponse();
            try
            {
                User user;

                using (DAUser daUser = new DAUser())
                {
                    user = daUser.LoginUser(req.Email, $"{req.Email}:{req.Password}".GenerateSHA512());
      
                }

                if (user != null)
                {
                    res.User = user;
                    //res.Token = BuildToken(user);
                    res.IsSuccessful = true;
                }
                else
                {
                    res.IsSuccessful = false;
                    res.Errors.Add(new Error() { Code = ((int)ErrorCodes.UserNotFound).ToString(), Message = "Email or Password is incorrect" });
                }
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;
                res.Errors.Add(new Error() { Code = ((int)ErrorCodes.ErrorNotSpecific).ToString(), Message = "There was a problem. Please try again later" });
            }
            return res;
        }
    }
}
