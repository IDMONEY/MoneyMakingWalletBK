using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IDMONEY.IO.Business
{
    public class BSUser : BaseBS
    {

        public ResCreateUser CreateUser(ReqCreateUser req)
        {
            ResCreateUser res = new ResCreateUser();
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

                        user.Id = daUser.InsertUser(user);

                        res.User = user;
                        res.Token = BuildToken(user);
                        res.IsSuccessful = true;
                    }
                    else
                    {
                        res.IsSuccessful = false;
                        res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.Conflict).ToString(), Message = "That email is taken. Try another" });
                    }
                }
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;

                if (ex.Message.Contains("UK_users_emai"))
                {
                    res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.Conflict).ToString(), Message = "That email is taken. Try another" });
                }
                else
                {
                    res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.ErrorNotSpecific).ToString(), Message = "There was a problem. Please try again later" });
                }
            }
            return res;
        }

        public ResLoginUser Login(ReqLoginUser req)
        {
            ResLoginUser res = new ResLoginUser();
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
                    res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.ErrorNotFound).ToString(), Message = "Email or Password is incorrect" });
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
