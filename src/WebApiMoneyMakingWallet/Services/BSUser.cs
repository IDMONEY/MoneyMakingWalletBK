using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Entities;
using IDMONEY.IO.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IDMONEY.IO.Services
{
    public class BSUser : BaseBS
    {
        public BSUser(ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
        {
        }

        public BSUser() : base()
        {
                
        }

        public CreateUserResponse CreateUser(ReqCreateUser req)
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
                            Password = GenerateSHA512String(req.Email, req.Password),
                            //Privatekey = privateKey
                        };

                        user.AvailableBalance = 0;
                        user.BlockedBalance = 0;
                        user.Id = daUser.InsertUser(user);

                        res.User = user;
                        res.Token = BuildToken(user);
                        res.IsSuccessful = true;
                    }
                    else
                    {
                        res.IsSuccessful = false;
                        res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.EmailIsRegistred).ToString(), Message = "That email is taken. Try another" });
                    }
                }
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;

                if (ex.Message.Contains("UK_users_emai"))
                {
                    res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.EmailIsRegistred).ToString(), Message = "That email is taken. Try another" });
                }
                else
                {
                    res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.ErrorNotSpecific).ToString(), Message = "There was a problem. Please try again later" });
                }
            }
            return res;
        }

        internal UserResponse GetUser(BaseRequest req)
        {
            UserResponse res = new UserResponse();
            using (DAUser daUser = new DAUser())
            {
                res.User = daUser.GetUser(User.Id);
            }
            res.IsSuccessful = true;
            return res;
        }

        public LoginUserResponse Login(ReqLoginUser req)
        {
            LoginUserResponse res = new LoginUserResponse();
            try
            {
                User user;

                using (DAUser daUser = new DAUser())
                {
                    user = daUser.LoginUser(req.Email, GenerateSHA512String(req.Email, req.Password));
                }

                if (user != null)
                {
                    res.User = user;
                    res.Token = BuildToken(user);
                    res.IsSuccessful = true;
                }
                else
                {
                    res.IsSuccessful = false;
                    res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.UserNotFound).ToString(), Message = "Email or Password is incorrect" });
                }
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;
                res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.ErrorNotSpecific).ToString(), Message = "There was a problem. Please try again later" });
            }
            return res;
        }
    }
}
