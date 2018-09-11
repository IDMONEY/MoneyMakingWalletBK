#region Libraries
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using IDMONEY.IO.Cryptography;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses; 
#endregion

namespace IDMONEY.IO.Users
{
    public class UserService : IUserService
    {
        #region Members
        private readonly IUserRepository userRepository;
        private readonly ITokenGenerator tokenGenerator;
        #endregion

        #region Constructor
        public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            Ensure.IsNotNull(userRepository);
            Ensure.IsNotNull(tokenGenerator);

            this.userRepository = userRepository;
            this.tokenGenerator = tokenGenerator;
        }
        #endregion

        #region Methods
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

        public UserResponse GetUser(ClaimsPrincipal claimsPrincipal)
        {
            UserResponse response = new UserResponse();
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

            if (claim.IsNotNull())
            {
                long userId = Convert.ToInt64(claim.Value);
                var user = this.userRepository.GetById(userId);

                response.User = user;
                response.IsSuccessful = true;
            }
            else
            {
                //TODO: USER NOT EXISTS
            }

            return response;
        } 
        #endregion
    }
}