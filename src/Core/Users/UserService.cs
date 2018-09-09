﻿using System;
using IDMONEY.IO.Cryptography;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;

namespace IDMONEY.IO.Users
{
    public class UserService : IUserService
    {
        #region Members
        private readonly IUserRepository userRepository;
        private readonly ITokenGenerator tokenGenerator;
        #endregion

        public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            this.userRepository = userRepository;
            this.tokenGenerator = tokenGenerator;
        }

        public CreateUserResponse Create(CreateUserRequest request)
        {
            CreateUserResponse response = new CreateUserResponse();

            var user = this.userRepository.GetByEmail(request.Email);


            if (user == null)
            {
                //var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
                //var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
                //var account = new Nethereum.Web3.Accounts.Account(privateKey);

                user = new User()
                {
                    //Address = account.Address,
                    Email = request.Email,
                    Password = $"{request.Email}:{request.Password}".GenerateSHA512()
                    //Privatekey = privateKey
                };

                user.AvailableBalance = 0;
                user.BlockedBalance = 0;
                user.Id = this.userRepository.Add(user);

                response.User = user;
                response.Token = this.tokenGenerator.Generate(user.Id.ToString());
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.Errors.Add(new Error() { Code = ((int)ErrorCodes.EmailIsRegistred).ToString(), Message = "That email is taken. Try another" });
            }



            return response;
        }
    }
}