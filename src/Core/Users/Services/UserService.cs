#region Libraries
using IDMONEY.IO.Security;
using System.Security.Claims;
using IDMONEY.IO.Cryptography;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Exceptions;
using System.Threading.Tasks;
#endregion

namespace IDMONEY.IO.Users
{
    public class UserService : IUserService
    {
        #region Members
        private readonly IUserRepository userRepository;
        private readonly ITokenGenerator tokenGenerator;
        private readonly INicknameRepository nicknameRepository;

        #endregion

        #region Constructor
        public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator, INicknameRepository nicknameRepository)
        {
            Ensure.IsNotNull(userRepository);
            Ensure.IsNotNull(tokenGenerator);
            Ensure.IsNotNull(nicknameRepository);

            this.userRepository = userRepository;
            this.tokenGenerator = tokenGenerator;
            this.nicknameRepository = nicknameRepository;
        }
        #endregion

        #region Methods
        public async Task<CreateUserResponse> CreateAsync(CreateUserRequest request)
        {
            CreateUserResponse response = new CreateUserResponse();

            var user = await this.userRepository.GetByEmailAsync(request.Email);


            if (user.IsNull())
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


                if (request.Nickname.IsNotNullOrEmpty())
                {
                    var userByNickName = await this.userRepository.GetByNicknameAsync(request.Nickname);

                    if (userByNickName.IsNotNull())
                    {
                        response.IsSuccessful = false;
                        response.Errors.Add(new Error() { Code = ((int)ErrorCodes.NicknameAlreadyRegistred).ToString(), Message = "That nickname is taken. Try another" });

                    }
                }

                if (response.IsSuccessful)
                {
                    //TODO: Check if account must be assigned
                    user.Id = await this.userRepository.AddAsync(user);

                    if (request.Nickname.IsNotNullOrEmpty())
                    {
                        await this.nicknameRepository.AddAsync(user, NickName.Create(request.Nickname));
                    }

                    response.User = user;
                    response.Token = this.tokenGenerator.Generate(user.Id.ToString());
                }
            }
            else
            {
                response.IsSuccessful = false;
                response.Errors.Add(new Error() { Code = ((int)ErrorCodes.EmailAlreadyRegistred).ToString(), Message = "That email is taken. Try another" });
            }



            return response;
        }

        public async Task<UserResponse> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            UserResponse response = new UserResponse();

            long userId = claimsPrincipal.GetUserId();
            var user = await this.userRepository.GetByIdAsync(userId);

            if (user.IsNull())
            {
                throw new NotFoundException("User not found");
            }

            response.User = user;
            return response;
        }
        #endregion
    }
}